using System.Net.Http.Json;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services.Implementations
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WebAPI");
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                return default;
            }
            catch
            {
                return default;
            }
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest payload)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, payload);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TResponse>();
                }
                return default;
            }
            catch
            {
                return default;
            }
        }

        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest payload)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(endpoint, payload);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TResponse>();
                }
                return default;
            }
            catch
            {
                return default;
            }
        }

        public async Task<TResponse?> DeleteAsync<TResponse>(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TResponse>();
                }
                return default;
            }
            catch
            {
                return default;
            }
        }
    }
}
