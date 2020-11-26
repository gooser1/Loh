using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loh.Backend.Model;
using Loh.Backend.Model.Enums;

namespace Loh.Backend.Game
{
    public class Bot
    {
        private GameRoom _room;
        private Guid _playerId;
        private Task _doingTask;
        private bool _stopped = false;
        public Bot(GameRoom room, Guid playerId)
        {
            _room = room;
            _playerId = playerId;
            _doingTask = Task.Run(Doing);
        }

        private async void Doing()
        {
            var rnd = new Random();
            while (!_stopped)//через случайный интервал кидает на стол подходящую карту или, если не может, загребает/говорит, что больше не подкинет
            {
                try
                {
                    var cards = _room.GetHand(_playerId, out _);
                    if (cards.Any())
                    {
                        var threwCard = false;
                        for (int i = 0; i < cards.Count; i++)
                        {
                            if (_room.MoveCardFromHandToTable(_playerId, cards[i], out _))
                            {
                                threwCard = true;
                                break;
                            }
                        }

                        if (!threwCard)
                        {
                            var status = _room.GetPlayerStatus(_playerId);

                            if (status == PlayerStatus.Attack && _room.IsAllProtected
                            ) //если подкинуть нечего, то сообщает об этом
                                _room.SetPlayerIsFinished(_playerId);

                            if (status == PlayerStatus.Defend && !_room.IsAllProtected
                            ) //если дальше крыть не способен - забирает
                                _room.SetPlayerIsFinished(_playerId);
                        }
                    }

                }
                catch (Exception e)
                {

                }
                await Task.Delay(rnd.Next(5000));
            }
        }

        public void Stop()
        {
            _stopped = true;
        }
    }
}
