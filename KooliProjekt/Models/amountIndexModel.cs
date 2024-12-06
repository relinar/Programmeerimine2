using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using System.Collections.Generic;

namespace KooliProjekt.Models
{
    public class AmountIndexModel
    {
        public amountSearch Search { get; set; }
        public PagedResult<Amount> Data { get; set; }
    }
}
