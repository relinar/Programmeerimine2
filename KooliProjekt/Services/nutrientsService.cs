using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class NutrientsService : INutrientsService
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the ApplicationDbContext
        public NutrientsService(ApplicationDbContext context)
        {
            _context = context;
        }

        // List method to return a paginated list of Nutrients
        public async Task<PagedResult<Nutrients>> List(int page, int pageSize)
        {
            return await _context.nutrients.GetPagedAsync(page, pageSize); // Assuming you have a custom GetPagedAsync extension method for pagination
        }

        // Get method to retrieve a specific Nutrients item by id
        public async Task<Nutrients> Get(int id)
        {
            return await _context.nutrients.FirstOrDefaultAsync(m => m.Id == id); // Fetches Nutrients item by id
        }

        // Save method to either add a new Nutrients or update an existing one
        public async Task Save(Nutrients item)
        {
            if (item.Id == 0)
            {
                _context.Add(item); // Adds a new Nutrients record
            }
            else
            {
                _context.Update(item); // Updates an existing Nutrients record
            }

            await _context.SaveChangesAsync(); // Save changes to the database
        }

        // Delete method to remove a Nutrients item by id
        public async Task Delete(int id)
        {
            var nutrients = await _context.nutrients.FindAsync(id); // Find Nutrients item by id
            if (nutrients != null)
            {
                _context.nutrients.Remove(nutrients); // Removes the item from the database
                await _context.SaveChangesAsync(); // Save changes
            }
        }
    }
}
