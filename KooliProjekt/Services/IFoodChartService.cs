using KooliProjekt.Data;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IFoodChartService
    {
        Task<PagedResult<FoodChart>> List(int page, int pageSize);

        Task<FoodChart> Get(int id);

        Task Save(FoodChart list);

        Task Delete(int id);
    }
}
