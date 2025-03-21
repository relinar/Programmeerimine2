using System.Net.Http;
using System.Net.Http.Json;

namespace KooliProjekt.WinFormsApp.Api
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
                var response = await _httpClient.GetAsync("Amounts");

                if (response.IsSuccessStatusCode)
                {
                    result.Value = await response.Content.ReadFromJsonAsync<List<Amount>>();
                }
                else
                {
                    result.Error = $"Serveri viga: {response.StatusCode}";
                }
            }
            catch (HttpRequestException ex)
            {
                result.Error = ex.Message.Contains("Connection")
                    ? "Ei saa serveriga ühendust. Palun proovi hiljem uuesti."
                    : ex.Message;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }

        public async Task<Result<bool>> Save(Amount amount)
        {
            var result = new Result<bool>();

            try
            {
                HttpResponseMessage response;
                if (amount.AmountID == 0)
                {
                    response = await _httpClient.PostAsJsonAsync("Amounts", amount);
                }
                else
                {
                    response = await _httpClient.PutAsJsonAsync($"Amounts/{amount.AmountID}", amount);
                }

                result.Value = response.IsSuccessStatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    result.Error = $"Salvestamine ebaõnnestus: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var result = new Result<bool>();

            try
            {
                var response = await _httpClient.DeleteAsync($"Amounts/{id}");

                result.Value = response.IsSuccessStatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    result.Error = $"Kustutamine ebaõnnestus: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }
    }
}
