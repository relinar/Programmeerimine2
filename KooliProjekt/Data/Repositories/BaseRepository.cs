using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public class BaseRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task Delete(int id)
        {
            await _context.Set<T>()
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync();
        }

        public virtual async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<PagedResult<T>> List(int page, int pageSize)
        {
            return await _context.Set<T>().GetPagedAsync(page, pageSize);
        }

        public virtual async Task Save(T entity)
        {
            if(entity.Id == 0)
            {
                _context.Add(entity);
            }
            else
            {                 
                _context.Update(entity);
            }

            await _context.SaveChangesAsync();
        }
    }
}
