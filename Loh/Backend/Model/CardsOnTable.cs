using System.Collections.Generic;
using System.Linq;
using Loh.Backend.Extensions;
using Loh.Backend.Game;
using Loh.Backend.Model.Dto;

namespace Loh.Backend.Model
{
    public class CardsOnTable
    {
        private List<HitterAndProtector> _hittersAndProtectors = new List<HitterAndProtector>();

        public bool IsEmpty => !_hittersAndProtectors.Any();

        public bool IsAllProtected => _hittersAndProtectors.All(x => !(x.Protector is null));

        private Rules _rules;

        public CardsOnTable(Rules rules)
        {
            _rules = rules;
        }

        public bool TryAddHitter(Card hitter, out string message)
        {
            message = "";
            if (IsEmpty)
            {
                _hittersAndProtectors.Add(new HitterAndProtector(){Hitter = hitter});
                return true;
            }
            else
            {
                if (_hittersAndProtectors.Any(x => x.Hitter.Rank == hitter.Rank || x.Protector.Rank == hitter.Rank))
                {
                    _hittersAndProtectors.Add(new HitterAndProtector() { Hitter = hitter });
                    return true;
                }
                else
                {
                    message = $"Нельзя подкинуть '{hitter}', на столе нет карт '{hitter.Rank.GetDescription()}'";
                    return false;
                }
            }
        }

        public bool TryAddProtector(Card protector, out string message, CardDto hitter = null)// Card hitter = null
        {
            message = "";
            if (hitter != default)
            {
                var pair = _hittersAndProtectors.FirstOrDefault(x => x.Hitter.EqualToDto(hitter));
                if (pair == default || pair.Protector != default)
                {
                    message = $"Карту '{hitter}' крыть не надо";
                    return false;
                }

                if (!_rules.IsCanProtect(pair.Hitter, protector))
                {
                    message = $"'{pair.Hitter}' не может быть покрыт/а '{protector}'";
                    return false;
                }

                pair.Protector = protector;
                return true;
            }
            else
            {
                var pair = _hittersAndProtectors.FirstOrDefault(x => x.Protector == default && _rules.IsCanProtect(x.Hitter, protector));
                if (pair == default)
                {
                    message = $"'{protector}' не может покрыть ничего на столе";
                    return false;
                }

                pair.Protector = protector;
                return true;
            }
        }


        public IList<Card> TakeCards()
        {
            var cards = new List<Card>();
            cards.AddRange(_hittersAndProtectors.Select(p=>p.Hitter));
            cards.AddRange(_hittersAndProtectors.Select(p=>p.Protector));
            _hittersAndProtectors.Clear();
            return cards;
        }

        public CardsOnTableDto ToDto()
        {
            return new CardsOnTableDto()
            {
                HittersAndProtectors = _hittersAndProtectors.Select(b => b.ToDto())
            };
        }
    }
}
