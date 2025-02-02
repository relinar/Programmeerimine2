using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace KooliProjekt.Data.Repositories
{
    public class AmountRepository : IAmountRepository
    {
        private readonly ApplicationDbContext _context;

        public AmountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all amounts
        public async Task<List<Amount>> GetAllAsync()
        {
            return await _context.amount.ToListAsync();
        }

        // Get a specific amount by ID
        public async Task<Amount> GetAsync(int id)
        {
            return await _context.amount.FindAsync(id);
        }

        // Save or update an amount
        public async Task SaveAsync(Amount amount)
        {
            if (amount.AmountID == 0)
                _context.amount.Add(amount);
            else
                _context.amount.Update(amount);

            await _context.SaveChangesAsync();
        }

        // Delete an amount by ID
        public async Task DeleteAsync(int id)
        {
            var amount = await _context.amount.FindAsync(id);
            if (amount != null)
            {
                _context.amount.Remove(amount);
                await _context.SaveChangesAsync();
            }
        }

        // Enable LINQ queries
        public IQueryable<Amount> AsQueryable()
        {
            return _context.amount.AsQueryable();
        }
    }
}
