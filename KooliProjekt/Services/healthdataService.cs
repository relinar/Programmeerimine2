using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Models;
using KooliProjekt.Search;
using KooliProjekt.Services;

public class HealthDataService : IHealthDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public HealthDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<HealthData>> List(int page, int pageSize, HealthDataSearch searchModel)
    {
        return await _unitOfWork.HealthDataRepository.GetPagedAsync(page, pageSize, searchModel);
    }

    public async Task<HealthData> Get(int id)
    {
        return await _unitOfWork.HealthDataRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(HealthData healthData)
    {
        await _unitOfWork.HealthDataRepository.AddAsync(healthData);
    }

    public async Task UpdateAsync(HealthData healthData)
    {
        _unitOfWork.HealthDataRepository.Update(healthData);
    }

    public async Task DeleteAsync(int id)
    {
        var healthData = await _unitOfWork.HealthDataRepository.GetByIdAsync(id);
        if (healthData != null)
        {
            await _unitOfWork.HealthDataRepository.DeleteAsync(healthData);
        }
    }


    public async Task SaveAsync()
    {
        await _unitOfWork.CommitAsync();
    }
}
