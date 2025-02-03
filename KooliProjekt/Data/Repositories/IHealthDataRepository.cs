using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public interface IHealthDataRepository
    {
        Task<PagedResult<HealthData>> GetPagedAsync(int page, int pageSize, HealthDataSearch searchModel);
        Task<HealthData> GetByIdAsync(int id);
        Task<List<HealthData>> GetAllAsync();
        Task AddAsync(HealthData healthData);
        Task DeleteAsync(HealthData healthData);
        Task UpdateAsync(HealthData healthData);
        Task Update(HealthData healthData); // Add this line

    }
}
