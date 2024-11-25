using KooliProjekt.Data.Repositories;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<FoodChart> FoodCharts { get; }
        Task CommitAsync();
    }
}
