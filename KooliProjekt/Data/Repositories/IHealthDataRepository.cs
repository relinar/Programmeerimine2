using KooliProjekt.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public interface IHealthDataRepository
    {
        Task<HealthData> GetAsync(int id);  // Asynchronous Get method
        Task<List<HealthData>> ListAsync(); // Asynchronous List method
        Task AddAsync(HealthData healthData); // Asynchronous Add method
        Task DeleteAsync(int id); // Asynchronous Delete method
        Task UpdateAsync(HealthData healthData); // Asynchronous Update method
        Task SaveAsync(); // Asynchronous Save method (typically to save changes)
    }
}
