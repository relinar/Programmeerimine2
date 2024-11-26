using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class FoodChartService : IFoodChartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FoodChartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<FoodChart>> List(int page, int pageSize)
        {
            var results = await _unitOfWork.FoodCharts.List(page, pageSize);

            return results;
        }

        public async Task<FoodChart> Get(int id)
        {
            return await _unitOfWork.FoodCharts.Get(id);
        }

        public async Task Save(FoodChart foodChart)
        {
            await _unitOfWork.FoodCharts.Save(foodChart);
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.FoodCharts.Delete(id);
        }
    }
}
