using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

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
            try
            {
                var data = await _httpClient.GetFromJsonAsync<List<Amount>>("api/amounts");
                return new Result<List<Amount>> { Value = data! };
            }
            catch (HttpRequestException httpEx)
            {
                return new Result<List<Amount>> { ErrorMessage = $"HTTP error: {httpEx.Message}" };
            }
            catch (Exception ex)
            {
                return new Result<List<Amount>> { ErrorMessage = $"Unexpected error: {ex.Message}" };
            }
        }

        public async Task<Result<Amount>> Get(int id)
        {
            try
            {
                var data = await _httpClient.GetFromJsonAsync<Amount>($"api/amounts/{id}");
                return new Result<Amount> { Value = data! };
            }
            catch (HttpRequestException httpEx)
            {
                return new Result<Amount> { ErrorMessage = $"HTTP error: {httpEx.Message}" };
            }
            catch (Exception ex)
            {
                return new Result<Amount> { ErrorMessage = $"Unexpected error: {ex.Message}" };
            }
        }



        public async Task<Result> Save(Amount amount)
        {
            try
            {
                HttpResponseMessage response;

                if (amount.AmountID == 0)
                {
                    // New item — use POST
                    response = await _httpClient.PostAsJsonAsync("api/amounts", amount);
                }
                else
                {
                    // Existing item — use PUT
                    response = await _httpClient.PutAsJsonAsync($"api/amounts/{amount.AmountID}", amount);
                }

                if (response.IsSuccessStatusCode)
                {
                    return new Result();
                }
                else
                {
                    return new Result { ErrorMessage = $"Server returned {response.StatusCode}" };
                }
            }
            catch (HttpRequestException httpEx)
            {
                return new Result { ErrorMessage = $"HTTP error: {httpEx.Message}" };
            }
            catch (Exception ex)
            {
                return new Result { ErrorMessage = $"Unexpected error: {ex.Message}" };
            }
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/amounts/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return new Result();
                }
                else
                {
                    return new Result { ErrorMessage = $"Server returned {response.StatusCode}" };
                }
            }
            catch (HttpRequestException httpEx)
            {
                return new Result { ErrorMessage = $"HTTP error: {httpEx.Message}" };
            }
            catch (Exception ex)
            {
                return new Result { ErrorMessage = $"Unexpected error: {ex.Message}" };
            }
        }
    }
}
