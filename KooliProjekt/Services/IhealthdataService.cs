// File: Services/IHealthDataService.cs
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IHealthDataService
    {
        // Get a paginated list of health data with search filters
        Task<PagedResult<HealthData>> List(int page, int pageSize, HealthDataSearch search);

        // Get a specific health data record by its ID
        Task<HealthData> Get(int id);

        // Save a health data record (either add or update)
        Task Save(HealthData item);

        // Delete a health data record by ID
        Task Delete(int id);
    }
}
