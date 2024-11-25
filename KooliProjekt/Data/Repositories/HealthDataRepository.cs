namespace KooliProjekt.Data.Repositories
{
    public class HealthDataRepository : BaseRepository<HealthData>
    {
        // Constructor: Passes ApplicationDbContext to the base class constructor
        public HealthDataRepository(ApplicationDbContext context) : base(context) { }

        // You can implement additional methods specific to HealthData if necessary
    }
}
