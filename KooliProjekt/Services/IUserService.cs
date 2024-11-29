using KooliProjekt.Data;
using KooliProjekt.Search;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IUserService
    {
        // Method to list users with pagination and search
        Task<PagedResult<User>> List(int page, int pageSize, UserSearch search);

        // Method to retrieve a single user by their id
        Task<User> Get(int id);

        // Method to save (add or update) a user
        Task Save(User user);

        // Method to delete a user by their id
        Task Delete(int id);
    }
}
