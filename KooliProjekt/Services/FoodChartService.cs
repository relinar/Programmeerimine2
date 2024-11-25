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

        // Implement the List method from IFoodChartService interface
        public async Task<PagedResult<FoodChart>> List(int page, int pageSize)
        {
            // Get paged data from the repository
            var pagedData = await _unitOfWork.FoodCharts.GetPagedAsync(page, pageSize);

            // Create and return the PagedResult object
            return new PagedResult<FoodChart>
            {
                Results = pagedData.Results,    // List of FoodCharts
                RowCount = pagedData.RowCount,  // Total number of items in the database
                CurrentPage = pagedData.CurrentPage, // Current page number
                PageSize = pagedData.PageSize, // Size of each page
                PageCount = pagedData.PageCount // Total number of pages
            };
        }

        public async Task<FoodChart> Get(int id)
        {
            return await _unitOfWork.FoodCharts.GetByIdAsync(id);
        }

        public async Task Save(FoodChart foodChart)
        {
            if (foodChart.Id == 0)
            {
                await _unitOfWork.FoodCharts.AddAsync(foodChart);
            }
            else
            {
                await _unitOfWork.FoodCharts.UpdateAsync(foodChart);
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var foodChart = await _unitOfWork.FoodCharts.GetByIdAsync(id);
            if (foodChart != null)
            {
                await _unitOfWork.FoodCharts.RemoveAsync(foodChart);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new KeyNotFoundException($"FoodChart with id {id} not found.");
            }
        }
    }
}
