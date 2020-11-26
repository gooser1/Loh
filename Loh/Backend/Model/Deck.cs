using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loh.Backend.Extensions;
using Loh.Backend.Model.Enums;

namespace Loh.Backend.Model
{
    public class Deck
    {
        private List<Card> _cards;

        public Card Trump { get; private set; }

        public int CardsCount => _cards.Count;

        private object _lock = new object();
        public Deck()
        {
            _cards = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>())
            {
                if (suit == CardSuit.Default)
                    continue;

                foreach (var rank in Enum.GetValues(typeof(CardRank)).Cast<CardRank>())
                {
                    if(rank == default)
                        continue;

                    _cards.Add(new Card(rank,suit));
                }
            }
            /*
            foreach (var type in Enum.GetValues(typeof(CardType)).Cast<CardType>())
            {
                if(type.In(CardType.Default, CardType.Ordinary))
                    continue;

                _cards.Add(new Card(type));
            }*/

            _cards.Shuffle();
            Trump = _cards.Last();
        }

        public IList<Card> Take(int count)
        {
            lock(_lock)
                return _cards.TakeAndDelete(count);
        }
    }
}
