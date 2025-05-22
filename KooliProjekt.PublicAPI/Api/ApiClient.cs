using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KooliProjekt.PublicAPI
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
                    result.ErrorMessage = $"Server error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Result> Save(Amount amount)
        {
            var result = new Result();

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

                if (!response.IsSuccessStatusCode)
                {
                    result.ErrorMessage = $"Save failed: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();

            try
            {
                var response = await _httpClient.DeleteAsync($"Amounts/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    result.ErrorMessage = $"Delete failed: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
