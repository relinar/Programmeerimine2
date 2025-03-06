// File: Services/FoodChartService.cs
using KooliProjekt.Data;
using KooliProjekt.Models;
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

        public async Task<PagedResult<FoodChart>> List(int page, int pageSize, FoodChartSearch search)
        {            
            var query = _context.food_Chart.AsQueryable();

            // otsingu tingimused
            // 

            return await query.GetPagedAsync(page, pageSize);
        }


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
            if (foodChart == null)
            {
                return;
            }

            _context.food_Chart.Remove(foodChart);
            await _context.SaveChangesAsync();
        }
    }

}
