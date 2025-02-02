using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class HealthDataService : IHealthDataService
    {
        private readonly IHealthDataRepository _repository;

        public HealthDataService(IHealthDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<HealthData> Get(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<List<HealthData>> List()
        {
            return await _repository.ListAsync();
        }

        public async Task AddAsync(HealthData healthData)
        {
            await _repository.AddAsync(healthData);
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task UpdateAsync(HealthData healthData)
        {
            await _repository.UpdateAsync(healthData);
        }

        public async Task Save(HealthData healthData)
        {
            await _repository.SaveAsync();
        }
    }
}
