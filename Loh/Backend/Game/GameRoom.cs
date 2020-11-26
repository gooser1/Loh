using System;
using System.Collections.Generic;
using System.Linq;
using Loh.Backend.Extensions;
using Loh.Backend.Model;
using Loh.Backend.Model.Dto;
using Loh.Backend.Model.Enums;

namespace Loh.Backend.Game
{
    public class GameRoom
    {
        public string GameStatus { get; private set; } = "Не установлен";
        public bool IsAllProtected => _cardsOnTable.IsAllProtected;

        public DeckDto DeckDto => _deck.ToDto();

        private Deck _deck;
        private List<Player> _players;
        private CardsOnTable _cardsOnTable;
        private Rules _rules;

        private int _defendingPlayerIndex = 0;
        private int _attackingPlayerIndex = 0;

        private Player DefendingPlayer => _players.Count > _defendingPlayerIndex ? _players[_defendingPlayerIndex] : null;
        private Player AttackingPlayer => _players.Count > _attackingPlayerIndex ? _players[_attackingPlayerIndex] : null;


        private object _lock = new object();

        public GameRoom()
        {
            _deck = new Deck();
            _players = new List<Player>();

            _rules = new Rules();
            _rules.SetTrump(_deck.Trump);
            _cardsOnTable = new CardsOnTable(_rules);
        }

        public void NextMove(out string message)
        {
            lock (_lock)
            {
                if (!_players.Any())
                {
                    message = "Тут вообще ни одного игрока";
                }
                else if (_players.Count == 1)
                {
                    message = $"Сидит один {_players.First()}";
                }
                else
                {
                    if (_defendingPlayerIndex == 0 && _attackingPlayerIndex == 0)
                    {
                        _defendingPlayerIndex = 1;
                    }
                    else
                    {
                        _defendingPlayerIndex++;
                        if (_defendingPlayerIndex >= _players.Count)
                            _defendingPlayerIndex = 0;

                        _attackingPlayerIndex++;
                        if (_attackingPlayerIndex >= _players.Count)
                            _attackingPlayerIndex = 0;
                    }

                    _players.ForEach(p => p.IsFinished = false);
                    DefendingPlayer.IsFinished = true;
                    foreach (var p in _players.Where(p1 => p1.Hand.Count < _rules.CardsOnHand))
                    {
                        var needCount = _rules.CardsOnHand - p.Hand.Count;
                        p.Hand.AddRange(_deck.Take(needCount));
                    }

                    if (!IsEndGame)
                        message = $"Ходит '{AttackingPlayer}' на '{DefendingPlayer}'";
                    else
                        message = $"Игра окончена, лох: {_players.FirstOrDefault(p => p.Hand.Any())}";
                }

                GameStatus = message;
            }
        }

        public bool IsEndGame => _players.Count(p => p.Hand.Any()) < 2;

        public Guid AddPlayer(string name)
        {
            lock (_lock)
            {
                var player = new Player(name);
                _players.Add(player);
                player.Hand = _deck.Take(6).ToList();
                return player.UserId;
            }
        }

        public IList<CardDto> GetHand(Guid userId, out string message)
        {
            lock (_lock)
            {
                var player = _players.FirstOrDefault(x => x.UserId == userId);
                if (player is null)
                {
                    message = "Нет игрока с таким UserId";
                    return new List<CardDto>();
                }

                message = "";
                return player.Hand.ToDto();
            }
        }

        public bool MoveCardFromHandToTable(Guid userId, CardDto cardDto, out string message, CardDto hitterForProtect = null)
        {
            lock (_lock)
            {
                var player = _players.FirstOrDefault(x => x.UserId == userId);
                if (player is null)
                {
                    message = "Нет игрока с таким UserId";
                    return false;
                }

                var card = player.Hand.FirstOrDefault(c => c.EqualToDto(cardDto));
                if (card is null)
                {
                    message = $"Шулер! У игрока '{player}' не должно быть на руке карты '{cardDto}'";
                    return false;
                }

                if (DefendingPlayer is null) //todo получше признак выработать
                {
                    message = "Подожди, ещё никто не ходит";
                    return false;
                }

                //защита
                var isMoved = false;
                if (IsDefendingPlayer(userId))
                {
                    isMoved = _cardsOnTable.TryAddProtector(card, out message, hitterForProtect);
                    CheckIsEndMove();
                }
                else
                {
                    //нападение
                    if (_cardsOnTable.IsEmpty)
                    {
                        if (IsAttackingPlayer(userId))
                            isMoved = _cardsOnTable.TryAddHitter(card, out message);
                        else
                        {
                            message = $"Сейчас ходит '{AttackingPlayer}'!";
                            isMoved = false;
                        }
                    }
                    else
                    {
                        isMoved = _cardsOnTable.TryAddHitter(card, out message);
                    }
                }

                if (isMoved)
                {
                    player.Hand.Remove(card);
                    if (IsEndGame)
                    {
                        message = $"Игра окончена, лох: {_players.FirstOrDefault(p => p.Hand.Any())}";
                        GameStatus = message;
                    }

                }

                return isMoved;
            }
        }

        public PlayerStatus GetPlayerStatus(Guid guid)
        {
            lock (_lock)
            {
                if (IsDefendingPlayer(guid))
                    return PlayerStatus.Defend;

                if (IsAttackingPlayer(guid))
                    return PlayerStatus.Attack;

                if (_players.Any(p => p.UserId == guid))
                    return PlayerStatus.JustSit;

                return PlayerStatus.Default;
            }
        }

        /// <summary>
        /// Если отбивается - забирает карты  со стола на руки, иначе сообщает, что не будет подкидывать
        /// </summary>
        /// <param name="guid"></param>
        public void SetPlayerIsFinished(Guid guid)
        {
            lock (_lock)
            {
                var player = _players.FirstOrDefault(p => p.UserId == guid);
                player.IsFinished = true;
                if (IsDefendingPlayer(guid))
                {
                    player.Hand.AddRange(_cardsOnTable.TakeCards());
                    return;
                }
                else
                {
                    CheckIsEndMove();
                }
            }
        }


        /// <summary>
        /// проверить, что всё отбито и больше никто не будет подкидывать
        /// </summary>
        private void CheckIsEndMove()
        {
            if (_cardsOnTable.IsAllProtected && _players.All(p => p.IsFinished))
            {
                _cardsOnTable.TakeCards();
                NextMove(out _);
            }
        }

        /// <summary>
        /// проверить, ход этого ли сейчас игрока
        /// </summary>
        private bool IsAttackingPlayer(Guid guid)
        {
            return AttackingPlayer?.UserId == guid;
        }

        /// <summary>
        /// проверить, этот ли игрок сейчас отбивается
        /// </summary>
        private bool IsDefendingPlayer(Guid guid)
        {
            return DefendingPlayer?.UserId == guid;
        }

    }
}
