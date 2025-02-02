using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Models;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class AmountService : IAmountService
    {
        private readonly IUnitOfWork _unitOfWork;

        // Constructor injection for UnitOfWork
        public AmountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get all Amounts
        public async Task<List<Amount>> GetAmountsAsync()
        {
            return await _unitOfWork.Amounts.GetAllAsync();  // Use UnitOfWork to get all Amounts
        }

        // Get Amount by Id
        public async Task<Amount> Get(int id)
        {
            return await _unitOfWork.Amounts.GetAsync(id);  // Use UnitOfWork to get Amount by id
        }

        // Add a new Amount
        public async Task AddAmountAsync(Amount amount)
        {
            await _unitOfWork.Amounts.SaveAsync(amount);  // Use UnitOfWork to save Amount
        }

        // Update an existing Amount
        public async Task UpdateAmountAsync(Amount amount)
        {
            await _unitOfWork.Amounts.SaveAsync(amount);  // Use UnitOfWork to save (update) Amount
        }

        // Delete an Amount by Id
        public async Task Delete(int id)
        {
            await _unitOfWork.Amounts.DeleteAsync(id);  // Use UnitOfWork to delete Amount by id
        }

        // Save (either Add or Update) Amount
        public async Task Save(Amount amount)
        {
            await _unitOfWork.Amounts.SaveAsync(amount);  // Use UnitOfWork to save Amount
        }

        // List Amounts with pagination and search functionality
        public async Task<PagedResult<Amount>> List(int page, int pageSize, amountSearch search)
        {
            var query = _unitOfWork.Amounts.AsQueryable();  // Assuming Amounts has AsQueryable() method

            // Apply search filters here (based on the search object)

            var totalCount = await query.CountAsync();
            var results = await query
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            return new PagedResult<Amount>
            {
                Results = results,
                RowCount = totalCount,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }
    }
}
