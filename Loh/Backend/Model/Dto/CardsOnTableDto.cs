using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loh.Backend.Model.Dto
{
    public class CardsOnTableDto
    {
        public IEnumerable<HitterAndProtectorDto> HittersAndProtectors { get; set; }
    }
}
