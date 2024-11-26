namespace KooliProjekt.Data.Repositories
{
    public interface IFoodChartRepository
    {
        Task<PagedResult<FoodChart>> List(int page, int pageSize);
        Task<FoodChart> Get(int id);
        Task Save(FoodChart foodChart);
        Task Delete(int id);
    }
}