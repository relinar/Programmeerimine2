using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class NutrientsIndexModel
    {
        public NutrientsSearch Search { get; set; }
        public PagedResult<Nutrients> Data { get; set; }
    }
}
