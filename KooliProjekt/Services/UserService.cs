using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Models;
using KooliProjekt.Search;
using KooliProjekt.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    // Constructor injection
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<User>> List(int page, int pageSize, UserSearch search)
    {
        return await _unitOfWork.Users.List(page, pageSize, search);  // Use UnitOfWork to access Users repository
    }

    public async Task<User> Get(int id)
    {
        return await _unitOfWork.Users.Get(id);  // Use UnitOfWork to access Users repository
    }

    public async Task Save(User user)
    {
        await _unitOfWork.Users.Save(user);  // Use UnitOfWork to access Users repository
    }

    public async Task Delete(int id)
    {
        await _unitOfWork.Users.Delete(id);  // Use UnitOfWork to access Users repository
    }
}
