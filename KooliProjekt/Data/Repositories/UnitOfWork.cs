using KooliProjekt.Data.Repositories;
using KooliProjekt.Data;

namespace KooliProjekt.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IHealthDataRepository _healthDataRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            FoodCharts = new FoodChartRepository(_context);
            Nutrients = new NutrientsRepository(_context);
            Users = new UserRepository(_context);
            Amounts = new AmountRepository(_context);
        }
        public IHealthDataRepository HealthDataRepository =>
        _healthDataRepository ??= new HealthDataRepository(_context);
        
        public IFoodChartRepository FoodCharts { get; }
        public INutrientsRepository Nutrients { get; }
        public IUserRepository Users { get; }
        public IAmountRepository Amounts { get; }
        public IHealthDataRepository HealthData { get; } // Property for HealthDataRepository

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
