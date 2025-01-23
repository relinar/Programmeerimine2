using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
                // Filter by Name (text search)
                if (!string.IsNullOrWhiteSpace(search.Name))
                {
                    query = query.Where(nutrient => nutrient.Name.Contains(search.Name));
                }

                // Filter by Carbohydrates (assuming it's a float type, we check for a numeric value)
                if (!string.IsNullOrWhiteSpace(search.Carbohydrates) && float.TryParse(search.Carbohydrates, out float carbs))
                {
                    query = query.Where(nutrient => nutrient.Carbohydrates == carbs); // Exact match for carbohydrates
                }

                // Filter by Sugars (assuming it's a float type, we check for a numeric value)
                if (!string.IsNullOrWhiteSpace(search.Sugars) && float.TryParse(search.Sugars, out float sugars))
                {
                    query = query.Where(nutrient => nutrient.Sugars == sugars); // Exact match for sugars
                }

                // Filter by Fats (assuming it's a float type, we check for a numeric value)
                if (!string.IsNullOrWhiteSpace(search.Fats) && float.TryParse(search.Fats, out float fats))
                {
                    query = query.Where(nutrient => nutrient.Fats == fats); // Exact match for fats
                }
            }

            // Fetching the total row count for pagination
            var rowCount = await query.CountAsync();
            // Paginate the results
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
    }
}
