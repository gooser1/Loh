using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loh.Backend.Model.Dto;
using Loh.Backend.Model.Enums;

namespace Loh.Backend.Game
{
    public static class GameRoomManager
    {
        //агада, это пока синглтон, хочешь заново - ребутни сервер, удобненько
        private static GameRoom _instance;
        public static GameRoom Instance => _instance ??= NewGameRoom();


        //а ещё это синглплеер против тупого компуктера
        #region Singleplayer

        private static GameRoom NewGameRoom()
        {
            var r = new GameRoom();
            DefaultUserId = r.AddPlayer("user");
            _bot = new Bot(r, r.AddPlayer("pc"));
            r.NextMove(out _);
            return r;
        }

        private static Bot _bot;
        public static Guid DefaultUserId;

        public static IList<CardDto> GetHand(out string message)
        {
            return Instance.GetHand(DefaultUserId, out message);
        }

        internal static void SetPlayerIsFinished()
        {
            Instance.SetPlayerIsFinished(DefaultUserId);
        }

        public static bool MoveCardFromHandToTable(CardDto card, out string message, CardDto hitterForProtect = null)
        {
            return Instance.MoveCardFromHandToTable(DefaultUserId, card, out message, hitterForProtect);
        }

        public static PlayerStatus GetPlayerStatus()
        {
            return Instance.GetPlayerStatus(DefaultUserId);
        }

        public static string GetGameStatus()
        {
            return Instance.GameStatus;
        }

        public static DeckDto GetDeck()
        {
            return Instance.DeckDto;
        }
        #endregion

    }


}
