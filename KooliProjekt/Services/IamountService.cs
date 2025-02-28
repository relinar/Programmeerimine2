using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IAmountService
    {
        Task<List<Amount>> GetAmountsAsync();
        Task<Amount> Get(int id);
        Task AddAmountAsync(Amount item);
        Task UpdateAmountAsync(Amount item);
        Task Delete(int id);
        Task Save(Amount item);
        //Task<PagedResult<Amount>> List(int page, int pageSize);
        Task<PagedResult<Amount>> List(int page, int pageSize, amountSearch search);
    }
}
