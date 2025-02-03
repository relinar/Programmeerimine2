using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IHealthDataService
    {
        Task<PagedResult<HealthData>> List(int page, int pageSize, HealthDataSearch searchModel);
        Task<HealthData> Get(int id);
        Task AddAsync(HealthData healthData);
        Task UpdateAsync(HealthData healthData);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
