using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public class HealthDataRepository : IHealthDataRepository
    {
        private readonly ApplicationDbContext _context;

        public HealthDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HealthData> GetAsync(int id)
        {
            return await _context.health_data.FindAsync(id);
        }

        public async Task<List<HealthData>> ListAsync()
        {
            return await _context.health_data.ToListAsync();
        }

        public async Task AddAsync(HealthData healthData)
        {
            await _context.health_data.AddAsync(healthData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var healthData = await _context.health_data.FindAsync(id);
            if (healthData != null)
            {
                _context.health_data.Remove(healthData);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(HealthData healthData)
        {
            _context.health_data.Update(healthData);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
