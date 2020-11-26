namespace Loh.Backend.Model.Dto
{
    public class HitterAndProtectorDto
    {
        /// <summary>
        /// Отбивающая карта (защищающая, в паре сверху)
        /// </summary>
        public CardDto Protector { get; set; }

        /// <summary>
        /// Бьющая карта (атакующая, нижняя в паре)
        /// </summary>
        public CardDto Hitter { get; set; }


    }
}