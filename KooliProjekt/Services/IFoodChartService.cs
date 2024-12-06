using KooliProjekt.Data;
using KooliProjekt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IFoodChartService
    {
        Task<PagedResult<FoodChart>> List(int page, int pageSize, FoodChartSearch search);
        Task<FoodChart> Get(int id);
        Task Save(FoodChart foodChart);
        Task Delete(int id);
    }
}