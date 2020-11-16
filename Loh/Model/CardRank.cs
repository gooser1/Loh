using System.ComponentModel;

namespace Loh.Model
{
    public enum CardRank
    {
        Default = 0,

        [Description("Двойка")]
        Two = 2,

        [Description("Тройка")]
        Three = 3,

        [Description("Четверка")]
        Four = 4,

        [Description("Пятерка")]
        Five = 5,

        [Description("Шестерка")]
        Six = 6,

        [Description("Семерка")]
        Seven = 7,

        [Description("Восьмерка")]
        Eight = 8,

        [Description("Девятка")]
        Nine = 9,

        [Description("Десятка")]
        Ten = 10,

        [Description("Валет")]
        Jack = 11,

        [Description("Дама")]
        Queen = 12,

        [Description("Король")]
        King = 13,

        [Description("Туз")]
        Ace = 14,
    }
}
