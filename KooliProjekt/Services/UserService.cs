using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
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

        // Method to list users with pagination
        public async Task<PagedResult<User>> List(int page, int pageSize)
        {
            // Assuming you have a custom extension method GetPagedAsync for pagination
            return await _context.User.GetPagedAsync(page, pageSize); // Adjusted to Users DbSet
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
