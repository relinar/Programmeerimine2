using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class AmountService : IAmountService
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the ApplicationDbContext
        public AmountService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all Amounts
        public async Task<List<Amount>> GetAmountsAsync()
        {
            return await _context.amount.ToListAsync(); // Fetch all Amounts
        }

        // Get a single Amount by id
        public async Task<Amount> Get(int id)
        {
            return await _context.amount
                .FirstOrDefaultAsync(a => a.AmountID == id); // Fetch the Amount by id
        }

        // Add a new Amount
        public async Task AddAmountAsync(Amount item)
        {
            _context.Add(item);  // Adds a new Amount to the DbSet
            await _context.SaveChangesAsync();  // Save changes to the database
        }
      
        // Update an existing Amount
        public async Task UpdateAmountAsync(Amount item)
        {
            _context.Update(item);  // Marks the Amount as modified
            await _context.SaveChangesAsync();  // Save changes to the database
        }

        public async Task Save(Amount amount)
        {
            if (amount.AmountID == 0)
            {
                _context.Add(amount);
            }
            else
            {
                _context.Update(amount);
            }

            await _context.SaveChangesAsync();
        }

        // Delete an Amount by id
        public async Task Delete(int id)
        {
            var amount = await _context.amount.FindAsync(id); // Find the Amount by id
            if (amount != null)
            {
                _context.amount.Remove(amount);  // Remove the Amount from the DbSet
                await _context.SaveChangesAsync();  // Save changes to the database
            }
        }
        public async Task<PagedResult<Amount>> List(int page, int pageSize, amountSearch search)
        {
            var query = _context.amount.AsQueryable();

            // Apply filters based on search parameters

            if(search != null)
            { 
                if (search.AmountID.HasValue)
                {
                    query = query.Where(a => a.AmountID == search.AmountID);
                }

                if (search.NutrientsID.HasValue)
                {
                    query = query.Where(a => a.NutrientsID == search.NutrientsID);
                }

                if (search.AmountDate.HasValue)
                {
                    query = query.Where(a => a.AmountDate.Date == search.AmountDate.Value.Date);  // Date comparison to ignore time
                }
            }

            return await query.GetPagedAsync(page, pageSize);
        }

        //public async Task<PagedResult<Amount>> List(int page, int pageSize)
        //{
        //    var query = _context.amount.AsQueryable(); // Ensure _context is correctly injected
        //    var totalItems = await query.CountAsync();  // Count total items in DB

        //    var items = await query
        //        .Skip((page - 1) * pageSize)  // Skip previous pages
        //        .Take(pageSize)  // Take only pageSize results
        //        .ToListAsync();

        //    return new PagedResult<Amount>(page, pageSize, totalItems, items);
        //}


    }
}



