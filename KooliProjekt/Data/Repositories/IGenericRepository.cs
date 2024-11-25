// IGenericRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);  // <-- Add this line to the interface
        Task<List<FoodChart>> GetPagedAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
    }
}
