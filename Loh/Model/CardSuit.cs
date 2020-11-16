using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Loh.Model
{
    public enum CardSuit
    {
        /// <summary>
        /// Не используется, если пришла такая - значит где то косяк
        /// </summary>
        Default = 0,

        [Description("Буби")]
        Diamonds = 1,

        [Description("Черви")]
        Hearts = 2,

        [Description("Пики")]
        Spades = 3,

        [Description("Крести")]
        Clubs = 4,
    }
}
