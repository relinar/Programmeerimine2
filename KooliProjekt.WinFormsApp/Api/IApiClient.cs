namespace KooliProjekt.WinFormsApp.Api
{
    public interface IApiClient
    {
        Task<Result<List<Amount>>> List();
        Task<Result<bool>> Save(Amount list);  // Muudetud tagastustüüp
        Task<Result<bool>> Delete(int id);    // Muudetud tagastustüüp
    }
}
