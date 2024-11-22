using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class FoodChartService : IFoodChartService
    {
        private readonly ApplicationDbContext _context;

        public FoodChartService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementing List method (pagination)
        public async Task<PagedResult<FoodChart>> List(int page, int pageSize)
        {
            // Assuming GetPagedAsync is an extension method for paginated results
            return await _context.food_Chart.GetPagedAsync(page, pageSize);
        }

        // Implementing Get method
        public async Task<FoodChart> Get(int id)
        {
            return await _context.food_Chart.FirstOrDefaultAsync(m => m.Id == id);
        }

        // Implementing Save method
        public async Task Save(FoodChart list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        // Implementing Delete method
        public async Task Delete(int id)
        {
            var foodChart = await _context.food_Chart.FindAsync(id);
            if (foodChart != null)
            {
                _context.food_Chart.Remove(foodChart);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"FoodChart with id {id} not found.");
            }
        }
    }
}
