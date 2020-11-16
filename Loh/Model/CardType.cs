using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Loh.Model
{
    public enum CardType
    {
        Default = 0,

        [Description("")]
        Ordinary = 1,

        [Description("Красный джокер")]
        JokerRed = 2,

        [Description("Черный джокер")]
        JokerBlack = 3,

        [Description("Имба")]
        Imba = 4,

        [Description("Имба имбей (шестёрка)")]
        ImbaSix = 5,

        [Description("Имба имбей(десятка)")]
        ImbaTen = 6,

        [Description("Иллюзионист")]
        Illusionist = 7,
    }
}
