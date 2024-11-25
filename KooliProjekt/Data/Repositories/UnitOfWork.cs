using KooliProjekt.Data.Repositories;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            FoodCharts = new FoodChartRepository(_context);
        }

        public IGenericRepository<FoodChart> FoodCharts { get; }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
