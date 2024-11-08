using KooliProjekt.Data;

public interface IAmountService
{
    Task<List<Amount>> GetAmountsAsync();
    Task<Amount> GetAmountAsync(int id);
    Task AddAmountAsync(Amount item);
    Task UpdateAmountAsync(Amount item);
    Task DeleteAmountAsync(int id);
}
