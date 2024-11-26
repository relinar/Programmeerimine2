using KooliProjekt.Data.Repositories;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
                          IFoodChartRepository foodChartRepository)
        {
            _context = context;
            FoodCharts = foodChartRepository;
        }

        public IFoodChartRepository FoodCharts { get; }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
