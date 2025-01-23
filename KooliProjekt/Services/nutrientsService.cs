using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Models;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class NutrientsService : INutrientsService
    {
        private readonly INutrientsRepository _nutrientsRepository;

        public NutrientsService(INutrientsRepository nutrientsRepository)
        {
            _nutrientsRepository = nutrientsRepository;
        }

        public async Task<PagedResult<Nutrients>> List(int page, int pageSize, NutrientsSearch search)
        {
            return await _nutrientsRepository.List(page, pageSize, search);
        }

        public async Task<Nutrients> Get(int id)
        {
            return await _nutrientsRepository.Get(id);
        }

        public async Task Save(Nutrients nutrients)
        {
            await _nutrientsRepository.Save(nutrients);
        }

        public async Task Delete(int id)
        {
            await _nutrientsRepository.Delete(id);
        }
    }
}
