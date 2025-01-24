using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class HealthDataService : IHealthDataService
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the ApplicationDbContext
        public HealthDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to get a list of health data records with pagination
        public async Task<PagedResult<HealthData>> List(int page, int pageSize, HealthDataSearch search)
        {
            return await _context.health_data.GetPagedAsync(page, pageSize); // Assuming GetPagedAsync is implemented
        }

        // Method to get a specific health data record by its ID
        public async Task<HealthData> Get(int id)
        {
            return await _context.health_data.FirstOrDefaultAsync(m => m.Id == id);
        }

        // Method to save a health data record (add or update)
        public async Task Save(HealthData item)
        {
            if (item.Id == 0)
            {
                _context.Add(item); // Add a new health data record
            }
            else
            {
                _context.Update(item); // Update the existing health data record
            }

            await _context.SaveChangesAsync();
        }

        // Method to delete a health data record by ID
        public async Task Delete(int id)
        {
            var healthData = await _context.health_data.FindAsync(id); // Find the health data record by ID
            if (healthData != null)
            {
                _context.health_data.Remove(healthData); // Remove the record from the database
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            else
            {
                // Optionally, throw an exception if not found
                throw new KeyNotFoundException($"HealthData with id {id} not found.");
            }
        }
    }
}
