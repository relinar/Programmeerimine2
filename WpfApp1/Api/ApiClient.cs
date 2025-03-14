using System.Net.Http;
using System.Net.Http.Json;
using WpfApp1.Api;

namespace WpfApp1.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task<Result<List<Amount>>> List()
        {
            var result = new Result<List<Amount>>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<List<Amount>>("Amounts");
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }

        public async Task Save(Amount list)
        {
            if (list.Id == 0)
            {
                await _httpClient.PostAsJsonAsync("Amounts", list);
            }
            else
            {
                await _httpClient.PutAsJsonAsync("Amounts/" + list.Id, list);
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync("Amounts/" + id);
        }
    }
}