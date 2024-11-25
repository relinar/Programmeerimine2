// BaseRepository.cs
using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity); // <-- Implement RemoveAsync
        }

        public Task<List<FoodChart>> GetPagedAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        Task<List<T>> IGenericRepository<T>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<T> IGenericRepository<T>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<T>.AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<T>.UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<T>.RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        Task<List<FoodChart>> IGenericRepository<T>.GetPagedAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<T>.GetTotalCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}
