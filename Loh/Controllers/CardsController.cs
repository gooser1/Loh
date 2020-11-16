using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loh.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Loh.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CardsController> _logger;

        public CardsController(ILogger<CardsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Card> Get()
        {
            var deck = new Deck();
            return deck.GetAll();
        }
    }
}
