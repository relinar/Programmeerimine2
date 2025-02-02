using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Models;
using KooliProjekt.Services;

public class FoodChartService : IFoodChartService
{
    private readonly IUnitOfWork _unitOfWork;

    // Constructor injection
    public FoodChartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<FoodChart>> List(int page, int pageSize, FoodChartSearch search)
    {
        return await _unitOfWork.FoodCharts.List(page, pageSize, search);  // Use UnitOfWork to access FoodCharts repository
    }

    public async Task<FoodChart> Get(int id)
    {
        return await _unitOfWork.FoodCharts.Get(id);  // Use UnitOfWork to access FoodCharts repository
    }

    public async Task Save(FoodChart foodChart)
    {
        await _unitOfWork.FoodCharts.Save(foodChart);  // Use UnitOfWork to access FoodCharts repository
    }

    public async Task Delete(int id)
    {
        await _unitOfWork.FoodCharts.Delete(id);  // Use UnitOfWork to access FoodCharts repository
    }
}
