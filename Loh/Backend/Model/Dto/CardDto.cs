using Loh.Backend.Model.Enums;

namespace Loh.Backend.Model.Dto
{
    public class CardDto
    {
        /// <summary>
        /// Ранг карты, от двойки до туза, у спец карт default
        /// </summary>
        public CardRank Rank { get; set; }


        /// <summary>
        /// Масть карты, у спец карт default
        /// </summary>
        public CardSuit Suit { get; set; }

        /// <summary>
        /// Тип карты
        /// </summary>
        public CardType Type { get; set; }

        /// <summary>
        /// Текстовое описание карты
        /// </summary>
        public string Description { get; set; }

        public override string ToString() => Description;
    }
}
