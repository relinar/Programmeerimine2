using KooliProjekt.Models;

namespace KooliProjekt.Data.Repositories
{
    public interface INutrientsRepository
    {
        Task<PagedResult<Nutrients>> List(int page, int pageSize, NutrientsSearch search); // Change from GetAllAsync to List
        Task<List<Nutrients>> GetAllAsync();  // Add this line to your interface
        Task<Nutrients> GetByIdAsync(int id);
        Task<Nutrients> Get(int id);
        Task Save(Nutrients nutrients);
        Task Delete(int id);
    }
}