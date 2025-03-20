namespace WpfApp1.Api
{
    public interface IApiClient
    {
        Task<Result<List<Amount>>> List();
        Task Save(Amount list);
        Task Delete(int id);
    }
}