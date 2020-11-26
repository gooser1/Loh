using Loh.Backend.Extensions;
using Loh.Backend.Model.Dto;

namespace Loh.Backend.Model
{
    public class HitterAndProtector
    {
        /// <summary>
        /// Отбивающая карта (защищающая, в паре сверху)
        /// </summary>
        public Card Protector;

        /// <summary>
        /// Бьющая карта (атакующая, нижняя в паре)
        /// </summary>
        public Card Hitter;

        public HitterAndProtectorDto ToDto()
        {
            return new HitterAndProtectorDto()
            {
                Protector = Protector.ToDto(),
                Hitter = Hitter.ToDto()
            };
        }
    }
}
