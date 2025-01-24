using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IHealthDataService
    {
        Task<HealthData> Get(int id);
        Task Add(HealthData healthData);
        Task Delete(int id);
        Task Update(HealthData healthData);
    }
}
