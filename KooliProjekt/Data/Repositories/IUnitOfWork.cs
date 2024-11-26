using KooliProjekt.Data.Repositories;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        IFoodChartRepository FoodCharts { get; }
        Task CommitAsync();
    }
}
