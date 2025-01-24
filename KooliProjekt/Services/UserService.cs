using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the ApplicationDbContext
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to list users with pagination and search functionality
        public async Task<PagedResult<User>> List(int page, int pageSize, UserSearch search)
        {
            var query = _context.User.AsQueryable();

            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.Name))
                {
                    query = query.Where(u => u.Name.Contains(search.Name));
                }
                if (!string.IsNullOrEmpty(search.Role))
                {
                    query = query.Where(u => u.Role.Contains(search.Role));
                }
            }

            return await query.GetPagedAsync(page, pageSize);
        }

        // Method to get a single user by id
        public async Task<User> Get(int id)
        {
            return await _context.User.FirstOrDefaultAsync(m => m.Id == id); // Fetches a User by id
        }

        // Method to save (add or update) a user
        public async Task Save(User user)
        {
            if (user.Id == 0)
            {
                _context.Add(user); // Adds a new User
            }
            else
            {
                _context.Update(user); // Updates an existing User
            }

            await _context.SaveChangesAsync(); // Save changes to the database
        }

        // Method to delete a user by id
        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id); // Find the user by id
            if (user != null)
            {
                _context.Users.Remove(user); // Remove the user from the DbSet
                await _context.SaveChangesAsync(); // Save changes to the database
            }
        }
    }
}
