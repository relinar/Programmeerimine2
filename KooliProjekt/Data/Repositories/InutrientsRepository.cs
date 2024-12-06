using KooliProjekt.Models;

namespace KooliProjekt.Data.Repositories
{
    public interface InutrientsRepository
    {
        Task<PagedResult<Nutrients>> List(int page, int pageSize, NutrientsSearch search);
        Task<Nutrients> Get(int id);
        Task Save(Nutrients nutrients);
        Task Delete(int id);
    }
}