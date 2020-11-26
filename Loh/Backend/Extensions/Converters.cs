using System.Collections.Generic;
using System.Linq;
using Loh.Backend.Model;
using Loh.Backend.Model.Dto;

namespace Loh.Backend.Extensions
{
    public static class Converters
    {
        public static CardDto ToDto(this Card c)
        {
            return new CardDto()
                {
                    Description = c.Description, 
                    Rank = c.Rank, 
                    Type = c.Type, 
                    Suit = c.Suit
                };
        }

        public static IList<CardDto> ToDto(this IList<Card> cards)
        {
            return cards.Select(c=>c.ToDto()).ToList();
        }

        public static DeckDto ToDto(this Deck deck)
        {
            return new DeckDto()
            {
                Trump = deck.Trump.ToDto(),
                CardsCount = deck.CardsCount
            };
        }
    }
}
