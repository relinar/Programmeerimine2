using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class amountIndexModel
    {
        public amountSearch Search { get; set; }
        public PagedResult<Amount> Data { get; set; }
    }
}
