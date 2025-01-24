using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class HealthDataService : IHealthDataService
    {
        private readonly ApplicationDbContext _context;

        public HealthDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get a health data by Id
        public async Task<HealthData> Get(int id)
        {
            return await _context.health_data.FindAsync(id);  // Ensure the DbSet<HealthData> is used
        }

        // Update a health data record
        public async Task Update(HealthData healthData)
        {
            _context.health_data.Update(healthData);  // Use HealthData type here
            await _context.SaveChangesAsync();
        }

        // Add a new health data record
        public async Task Add(HealthData healthData)
        {
            await _context.health_data.AddAsync(healthData);  // Add HealthData to the DbSet
            await _context.SaveChangesAsync();
        }

        // Delete health data by Id
        public async Task Delete(int id)
        {
            var healthData = await _context.health_data.FindAsync(id);  // Find health data by Id
            if (healthData != null)
            {
                _context.health_data.Remove(healthData);  // Remove it from the DbSet
                await _context.SaveChangesAsync();
            }
        }
    }
}
