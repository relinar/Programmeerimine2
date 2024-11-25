namespace KooliProjekt.Data.Repositories
{
    public class NutrientRepository : BaseRepository<Nutrients>
    {
        // Constructor: Passes ApplicationDbContext to the base class constructor
        public NutrientRepository(ApplicationDbContext context) : base(context) { }

        // You can implement additional methods specific to Nutrients if necessary
    }
}
