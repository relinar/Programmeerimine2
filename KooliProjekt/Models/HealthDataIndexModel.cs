using KooliProjekt.Search;
using KooliProjekt.Models;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class HealthDataIndexModel
    {
        public PagedResult<HealthData> Data { get; set; }
        public HealthDataSearch Search { get; set; }
    }
}
