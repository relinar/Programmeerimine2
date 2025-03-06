using KooliProjekt.Models;
using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore; // Add this for CountAsync and ToListAsync
using System.Threading.Tasks;
using System.Linq;

namespace KooliProjekt.Services
{
    public class NutrientsService : INutrientsService
    {
        private readonly ApplicationDbContext _context;

        public NutrientsService(ApplicationDbContext context)
        {
            _context = context;
        }

        // List nutrients with pagination and search
        public async Task<PagedResult<Nutrients>> List(int page, int pageSize, NutrientsSearch search)
        {
            var query = _context.nutrients.AsQueryable();

            // Apply search filters if any
            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(n => n.Name.Contains(search.Name));
            }

            // Convert string to float for search fields and check for valid parsing
            if (!string.IsNullOrEmpty(search.Carbohydrates) && float.TryParse(search.Carbohydrates, out float carbs))
            {
                query = query.Where(n => n.Carbohydrates == carbs);
            }

            if (!string.IsNullOrEmpty(search.Fats) && float.TryParse(search.Fats, out float fats))
            {
                query = query.Where(n => n.Fats == fats);
            }

            if (!string.IsNullOrEmpty(search.Sugars) && float.TryParse(search.Sugars, out float sugars))
            {
                query = query.Where(n => n.Sugars == sugars);
            }

            return await query.GetPagedAsync(page, pageSize);
        }

        // Add a new nutrient
        public async Task Add(Nutrients nutrient)
        {
            _context.nutrients.Add(nutrient);
            await _context.SaveChangesAsync();
        }

        // Update an existing nutrient
        public async Task Update(Nutrients nutrient)
        {
            _context.nutrients.Update(nutrient);
            await _context.SaveChangesAsync();
        }

        // Delete a nutrient by ID
        public async Task Delete(int id)
        {
            var nutrient = await _context.nutrients.FindAsync(id);
            if (nutrient != null)
            {
                _context.nutrients.Remove(nutrient);
                await _context.SaveChangesAsync();
            }
        }

        // Get a specific nutrient by ID
        public async Task<Nutrients> Get(int id)
        {
            return await _context.nutrients.FindAsync(id);
        }
    }
}
