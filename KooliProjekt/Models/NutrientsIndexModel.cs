using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class NutrientsIndexModel
    {
        public PagedResult<Nutrients> Data { get; set; }
        public NutrientsSearch Search { get; set; }
    }
}
