namespace KooliProjekt.Data.Repositories
{
    public interface IAmountRepository
    {
        Task<List<Amount>> GetAllAsync();
        Task<Amount> GetAsync(int id);
        Task SaveAsync(Amount amount);
        Task DeleteAsync(int id);

        // Method to support pagination and filtering if needed
        IQueryable<Amount> AsQueryable();  // Add this method to enable LINQ queries
    }
}
