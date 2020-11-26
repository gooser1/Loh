using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Loh.Backend.Model;

namespace Loh.Backend.Game
{
    public class Rules
    {
        private Card _trump;

        public int CardsOnHand { get; private set; } = 6;

        public void SetTrump(Card card)
        {
            _trump = card;
        }

        public bool IsCanProtect(Card Hitter, Card Protector)
        {
            if (Protector.Suit == Hitter.Suit)
                return Protector.Rank > Hitter.Rank;
        
            return Protector.Suit == _trump.Suit;
        }
    }
}
