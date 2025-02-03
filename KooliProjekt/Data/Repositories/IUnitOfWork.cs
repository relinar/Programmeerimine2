namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        IFoodChartRepository FoodCharts { get; }
        INutrientsRepository Nutrients { get; }
        IUserRepository Users { get; }
        IAmountRepository Amounts { get; }
        IHealthDataRepository HealthDataRepository { get; }
        Task CommitAsync();
    }
}
