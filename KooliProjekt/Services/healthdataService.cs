// File: Services/HealthDataService.cs
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;  // Required for async methods
using System.Linq;
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

        public async Task<PagedResult<HealthData>> List(int page, int pageSize, HealthDataSearch search)
        {
            var query = _context.health_data.AsQueryable();

            // Apply filters based on search criteria
            if (!string.IsNullOrEmpty(search.User))
                query = query.Where(x => x.User.Contains(search.User));

            if (search.BloodSugar.HasValue)
                query = query.Where(x => x.BloodSugar == search.BloodSugar);

            if (search.Weight.HasValue)
                query = query.Where(x => x.Weight == search.Weight);

            if (!string.IsNullOrEmpty(search.BloodAir))
                query = query.Where(x => x.BloodAir.Contains(search.BloodAir));

            if (search.Systolic.HasValue)
                query = query.Where(x => x.Systolic == search.Systolic);

            if (search.Diastolic.HasValue)
                query = query.Where(x => x.Diastolic == search.Diastolic);

            if (!string.IsNullOrEmpty(search.Pulse))
                query = query.Where(x => x.Pulse.Contains(search.Pulse));

            if (search.Age.HasValue)
                query = query.Where(x => x.Age == search.Age);

            // Apply pagination
            var totalCount = await query.CountAsync();  // Async count
            var results = await query
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();  // Async ToList

            return new PagedResult<HealthData>
            {
                Results = results,
                RowCount = totalCount,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        public async Task<HealthData> Get(int id)
        {
            return await _context.health_data.FindAsync(id);  // This already works for async, no changes needed here.
        }

        public Task Save(HealthData item)
        {
            if (item.Id == 0)
                _context.health_data.Add(item);
            else
                _context.health_data.Update(item);

            return _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var entity = _context.health_data.Find(id);
            if (entity != null)
            {
                _context.health_data.Remove(entity);
                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
    }
}
