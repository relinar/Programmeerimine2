using System.Threading.Tasks;
using KooliProjekt.Models;
using KooliProjekt.Search;

namespace KooliProjekt.Data.Repositories
{
    public interface IUserRepository
    {
        Task<PagedResult<User>> List(int page, int pageSize, UserSearch search);
        Task<User> Get(int id); // ✅ Lisa Get meetod
        Task Save(User user); // ✅ Lisa Save meetod
        Task Delete(int id); // ✅ Lisa Delete meetod
    }
}
