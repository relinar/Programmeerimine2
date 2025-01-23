using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public class FoodChartRepository : BaseRepository<FoodChart>, IFoodChartRepository
    {
        public FoodChartRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<PagedResult<FoodChart>> List(int page, int pageSize, FoodChartSearch search)
        {
            var query = _context.food_Chart.AsQueryable();  // Query food_Chart table

            if (search != null)
            {
                // Filter by InvoiceNo
                if (!string.IsNullOrWhiteSpace(search.InvoiceNo))
                {
                    query = query.Where(foodChart => foodChart.InvoiceNo.Contains(search.InvoiceNo));
                }

                // Filter by User
                if (!string.IsNullOrWhiteSpace(search.user))
                {
                    query = query.Where(foodChart => foodChart.user.Contains(search.user));
                }

                // Filter by Meal
                if (!string.IsNullOrWhiteSpace(search.meal))
                {
                    query = query.Where(foodChart => foodChart.meal.Contains(search.meal));
                }

                // Filter by Date
                if (!string.IsNullOrWhiteSpace(search.date))
                {
                    if (DateTime.TryParse(search.date, out DateTime searchDate))
                    {
                        query = query.Where(foodChart => foodChart.date.Contains(search.date)); // Compare the date part only
                    }
                }
            }

            // Return paginated result
            return await query.GetPagedAsync(page, pageSize);
        }

        // Implement Get method
        public async Task<FoodChart> Get(int id)
        {
            return await _context.food_Chart
                .FirstOrDefaultAsync(fc => fc.Id == id); // Fetches a FoodChart by Id
        }

        // Implement Save method
        public async Task Save(FoodChart foodChart)
        {
            if (foodChart.Id == 0) // New FoodChart
            {
                await _context.food_Chart.AddAsync(foodChart);
            }
            else // Existing FoodChart, update
            {
                _context.food_Chart.Update(foodChart);
            }
            await _context.SaveChangesAsync(); // Save changes to database
        }

        // Implement Delete method
        public async Task Delete(int id)
        {
            var foodChart = await _context.food_Chart.FindAsync(id);
            if (foodChart != null)
            {
                _context.food_Chart.Remove(foodChart);
                await _context.SaveChangesAsync(); // Commit deletion to the database
            }
        }
    }
}
