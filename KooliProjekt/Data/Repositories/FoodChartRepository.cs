using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public class FoodChartRepository : IGenericRepository<FoodChart>
    {
        private readonly ApplicationDbContext _context;

        public FoodChartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get paginated list of FoodCharts
        public async Task<List<FoodChart>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.food_Chart
                .Skip((page - 1) * pageSize) // Skipping the already fetched items
                .Take(pageSize)             // Taking the required number of items
                .ToListAsync();
        }

        // Get total count of FoodCharts
        public async Task<int> GetTotalCountAsync()
        {
            return await _context.food_Chart.CountAsync();
        }

        public async Task<FoodChart> GetByIdAsync(int id)
        {
            return await _context.food_Chart.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(FoodChart entity)
        {
            await _context.food_Chart.AddAsync(entity);
        }

        public async Task UpdateAsync(FoodChart entity)
        {
            _context.food_Chart.Update(entity);
        }

        public async Task RemoveAsync(FoodChart entity)
        {
            _context.food_Chart.Remove(entity);
        }

        Task<List<FoodChart>> IGenericRepository<FoodChart>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<FoodChart> IGenericRepository<FoodChart>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<FoodChart>.AddAsync(FoodChart entity)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<FoodChart>.UpdateAsync(FoodChart entity)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<FoodChart>.RemoveAsync(FoodChart entity)
        {
            throw new NotImplementedException();
        }

        Task<List<FoodChart>> IGenericRepository<FoodChart>.GetPagedAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<FoodChart>.GetTotalCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}
