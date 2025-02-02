using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        IFoodChartRepository FoodCharts { get; }
        IHealthDataRepository HealthData { get; }
        INutrientsRepository Nutrients { get; }
        IUserRepository Users { get; }
        IAmountRepository Amounts { get; } // ✅ Lisa Amounts repository

        Task CommitAsync();
    }
}
