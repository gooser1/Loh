namespace Loh.Backend.Model.Dto
{
    public class DeckDto
    {
        /// <summary>
        /// Козырь
        /// </summary>
        public CardDto Trump { get; set; }

        /// <summary>
        /// Количество оставшихся карт
        /// </summary>
        public int CardsCount { get; set; }
    }
}
