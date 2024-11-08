using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

public class AmountService : IAmountService
{
    private readonly ApplicationDbContext _context;

    public AmountService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Amount>> GetAmountsAsync()
    {
        return await _context.amount.ToListAsync();
    }

    public async Task<Amount> GetAmountAsync(int id)
    {
        return await _context.amount.FindAsync(id);
    }

    public async Task AddAmountAsync(Amount item)
    {
        _context.amount.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAmountAsync(Amount item)
    {
        _context.amount.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAmountAsync(int id)
    {
        var amount = await _context.amount.FindAsync(id);
        if (amount != null)
        {
            _context.amount.Remove(amount);
            await _context.SaveChangesAsync();
        }
    }
}
