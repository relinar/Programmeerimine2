// File: Services/FoodChartService.cs
using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Models;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class FoodChartService : IFoodChartService
    {
        private readonly IFoodChartRepository _foodChartRepository;

        // Constructor injection
        public FoodChartService(IFoodChartRepository foodChartRepository)
        {
            _foodChartRepository = foodChartRepository;
        }

        // List method that gets paginated results from repository
        public async Task<PagedResult<FoodChart>> List(int page, int pageSize, FoodChartSearch search)
        {
            return await _foodChartRepository.List(page, pageSize, search);
        }

        // Method to get a specific FoodChart by ID
        public async Task<FoodChart> Get(int id)
        {
            return await _foodChartRepository.Get(id);
        }

        // Save method to insert or update a FoodChart
        public async Task Save(FoodChart foodChart)
        {
            await _foodChartRepository.Save(foodChart);
        }

        // Delete method to remove a FoodChart by ID
        public async Task Delete(int id)
        {
            await _foodChartRepository.Delete(id);  
        }
    }
}
