using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<HealthData> GetByIdAsync(int id)
        {
            return await _context.health_data.FindAsync(id);
        }

        public async Task<List<HealthData>> GetAllAsync()
        {
            return await _context.health_data.ToListAsync(); // Using correct DbSet name
        }

        public async Task AddAsync(HealthData healthData)
        {
            await _context.health_data.AddAsync(healthData); // Using correct DbSet name
        }

        public async Task DeleteAsync(HealthData healthData)
        {
            _context.health_data.Remove(healthData); // Using correct DbSet name
        }

        public async Task UpdateAsync(HealthData healthData)
        {
            _context.health_data.Update(healthData); // Using correct DbSet name
        }

        // Corrected synchronous Update method to return Task
        public async Task Update(HealthData healthData)
        {
            _context.health_data.Update(healthData); // Using correct DbSet name
            await Task.CompletedTask; // Ensuring the method returns Task
        }

        // GetPagedAsync method to retrieve paged results with filtering
        public async Task<PagedResult<HealthData>> GetPagedAsync(int page, int pageSize, HealthDataSearch searchModel)
        {
            var query = _context.health_data.AsQueryable();

            // Add filtering logic based on searchModel (e.g., search by date, user, etc.)
            if (!string.IsNullOrEmpty(searchModel.User))
            {
                query = query.Where(h => h.User.Contains(searchModel.User));
            }

            var totalCount = await query.CountAsync(); // Get the total number of records matching the search

            var pageCount = (int)Math.Ceiling(totalCount / (double)pageSize); // Calculate total page count

            var results = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(); // Fetch the records for the current page

            return new PagedResult<HealthData>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = totalCount,
                PageCount = pageCount,
                Results = results // Return the actual data
            };
        }
    }
}
