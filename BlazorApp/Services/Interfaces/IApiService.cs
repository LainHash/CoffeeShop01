namespace BlazorApp.Services.Interfaces
{
    public interface IApiService
    {
        Task<T?> GetAsync<T>(string endpoint);
        Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest payload);
        Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest payload);
        Task<TResponse?> DeleteAsync<TResponse>(string endpoint);
    }
}
