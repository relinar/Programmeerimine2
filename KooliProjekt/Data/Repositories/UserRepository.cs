using System.Threading.Tasks;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using KooliProjekt.Search;

namespace KooliProjekt.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<User>> List(int page, int pageSize, UserSearch search)
        {
            var query = _context.User.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
                query = query.Where(u => u.Name.Contains(search.Name));

            if (!string.IsNullOrEmpty(search.Role))
                query = query.Where(u => u.Role.Contains(search.Role));

            return await query.GetPagedAsync(page, pageSize);
        }

        public async Task<User> Get(int id) => await _context.User.FindAsync(id); // ✅ Lisa Get meetod

        public async Task Save(User user) // ✅ Lisa Save meetod
        {
            if (user.Id == 0)
                _context.User.Add(user);
            else
                _context.User.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id) // ✅ Lisa Delete meetod
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
