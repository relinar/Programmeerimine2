using KooliProjekt.Data.Repositories;
using KooliProjekt.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context,
                      IFoodChartRepository foodChartRepository,
                      IHealthDataRepository healthDataRepository,
                      INutrientsRepository nutrientsRepository,
                      IUserRepository userRepository,
                      IAmountRepository amountRepository) // ✅ Lisa Amounts repository
    {
        _context = context;
        FoodCharts = foodChartRepository;
        HealthData = healthDataRepository;
        Nutrients = nutrientsRepository;
        Users = userRepository;
        Amounts = amountRepository; // ✅ Lisa Amounts repository
    }

    public IFoodChartRepository FoodCharts { get; }
    public IHealthDataRepository HealthData { get; }
    public INutrientsRepository Nutrients { get; }
    public IUserRepository Users { get; }
    public IAmountRepository Amounts { get; } // ✅ Lisa Amounts repository

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
