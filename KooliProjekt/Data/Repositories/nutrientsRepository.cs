using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic; // Import List

namespace KooliProjekt.Data.Repositories
{
    public class NutrientsRepository : BaseRepository<Nutrients>, INutrientsRepository
    {
        public NutrientsRepository(ApplicationDbContext context) : base(context)
        { }

        // List method with pagination and filtering
        public async Task<PagedResult<Nutrients>> List(int page, int pageSize, NutrientsSearch search)
        {
            var query = _context.Nutrients.AsQueryable(); // Start query

            // Apply filters if present in the search model
            if (search != null)
            {
                if (!string.IsNullOrWhiteSpace(search.Name))
                {
                    query = query.Where(nutrient => nutrient.Name.Contains(search.Name));
                }
                if (!string.IsNullOrWhiteSpace(search.Carbohydrates) && float.TryParse(search.Carbohydrates, out float carbs))
                {
                    query = query.Where(nutrient => nutrient.Carbohydrates == carbs);
                }
                if (!string.IsNullOrWhiteSpace(search.Sugars) && float.TryParse(search.Sugars, out float sugars))
                {
                    query = query.Where(nutrient => nutrient.Sugars == sugars);
                }
                if (!string.IsNullOrWhiteSpace(search.Fats) && float.TryParse(search.Fats, out float fats))
                {
                    query = query.Where(nutrient => nutrient.Fats == fats);
                }
            }

            var rowCount = await query.CountAsync();
            var results = await query.Skip((page - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

            return new PagedResult<Nutrients>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = rowCount,
                PageCount = (int)Math.Ceiling((double)rowCount / pageSize),
                Results = results
            };
        }

        public async Task<Nutrients> Get(int id)
        {
            return await _context.Nutrients.FindAsync(id);
        }

        public async Task Save(Nutrients nutrients)
        {
            await SaveAsync(nutrients);
        }

        public async Task Delete(int id)
        {
            var nutrient = await Get(id);
            if (nutrient != null)
            {
                await DeleteAsync(nutrient);
            }
        }

        // Implement GetAllAsync to fetch all nutrients
        public async Task<List<Nutrients>> GetAllAsync()
        {
            return await _context.Nutrients.ToListAsync(); // Fetch all nutrients
        }
    }
}
