using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loh.Backend.Model.Enums
{
    public enum PlayerStatus
    {
        Default = 0,

        /// <summary>
        /// Ходи
        /// </summary>
        Attack = 1,

        /// <summary>
        /// Отбивайся
        /// </summary>
        Defend = 2,

        /// <summary>
        /// Подбрасывай
        /// </summary>
        JustSit = 3
    }
}
