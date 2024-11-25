namespace KooliProjekt.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        // Constructor: Passes ApplicationDbContext to the base class constructor
        public UserRepository(ApplicationDbContext context) : base(context) { }

        // You can implement additional methods specific to User if necessary
    }
}
