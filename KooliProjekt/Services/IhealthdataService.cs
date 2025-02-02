using KooliProjekt.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IHealthDataService
    {
        Task<HealthData> Get(int id);
        Task<List<HealthData>> List();
        Task AddAsync(HealthData healthData);
        Task Delete(int id);
        Task UpdateAsync(HealthData healthData);
        Task Save(HealthData healthData);
    }
}
