namespace KooliProjekt.WinFormsApp.Api
{
    public interface IApiClient
    {
        Task<Result<List<Amount>>> List();
        Task Save(Amount list);
        Task Delete(int id);
    }
}