using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Models;
using KooliProjekt.Services;

public class NutrientsService : INutrientsService
{
    private readonly IUnitOfWork _unitOfWork;

    // Constructor injection
    public NutrientsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<Nutrients>> List(int page, int pageSize, NutrientsSearch search)
    {
        return await _unitOfWork.Nutrients.List(page, pageSize, search);  // Use UnitOfWork to access Nutrients repository
    }

    public async Task<Nutrients> Get(int id)
    {
        return await _unitOfWork.Nutrients.Get(id);  // Use UnitOfWork to access Nutrients repository
    }

    public async Task Save(Nutrients nutrients)
    {
        await _unitOfWork.Nutrients.Save(nutrients);  // Use UnitOfWork to access Nutrients repository
    }

    public async Task Delete(int id)
    {
        await _unitOfWork.Nutrients.Delete(id);  // Use UnitOfWork to access Nutrients repository
    }
}
