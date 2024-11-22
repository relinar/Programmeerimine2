using KooliProjekt.Data;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface INutrientsService
    {
        // Asynchronous method to get a paginated list of Nutrients
        Task<PagedResult<Nutrients>> List(int page, int pageSize);

        // Asynchronous method to get a specific Nutrients item by id
        Task<Nutrients> Get(int id);

        // Asynchronous method to save (add or update) a Nutrients item
        Task Save(Nutrients item);

        // Asynchronous method to delete a Nutrients item by id
        Task Delete(int id);
    }
}
