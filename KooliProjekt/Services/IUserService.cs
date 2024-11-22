using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IUserService
    {
        // Method to retrieve a paginated list of users
        Task<PagedResult<User>> List(int page, int pageSize);

        // Method to retrieve a single user by their id
        Task<User> Get(int id);

        // Method to save (add or update) a user
        Task Save(User user);

        // Method to delete a user by their id
        Task Delete(int id);
    }
}
