using System.Collections.Generic;
using System.Threading.Tasks;
using Loh.Backend.Game;
using Loh.Backend.Model.Dto;
using Loh.Backend.Model.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Loh.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;

        public RoomController(ILogger<RoomController> logger)
        {
            _logger = logger;
        }

        [HttpGet("deck")]
        public DeckDto GetDeck()
        {
            return GameRoomManager.GetDeck();
        }

        [HttpGet("myHand")]
        public IEnumerable<CardDto> GetHand()
        {
            return GameRoomManager.GetHand(out _);
        }

        [HttpGet("myStatus")]
        public PlayerStatus GetPlayerStatus()
        {
            return GameRoomManager.GetPlayerStatus();
        }

        [HttpGet("gameStatus")]
        public string GetGameStatus()
        {
            return GameRoomManager.GetGameStatus();
        }

        [HttpPost("cardToTable")]
        public async Task<ActionResult<CardDto>> PostCard(CardDto card)
        {
            if (GameRoomManager.MoveCardFromHandToTable(card, out var message))
            {
                return Accepted();
            }
            else
            {
                return ValidationProblem(message);
            }
        }

        [HttpPost("imFinish")]
        public async Task<ActionResult> SetPlayerIsFinished()
        {
            GameRoomManager.SetPlayerIsFinished();
            return Accepted();
        }
    }
}
