using BlazorApp.Models.Auth;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services.Implementations
{
    public class AuthApiService : IAuthApiService
    {
        private readonly IApiService _apiService;

        public AuthApiService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<AuthApiResponse?> LoginAsync(LoginInput input)
        {
            return await _apiService.PostAsync<LoginInput, AuthApiResponse>("/api/Auth/login", input);
        }

        public async Task<AuthApiResponse?> RegisterEmployeeAsync(RegisterEmployeeInput input)
        {
            return await _apiService.PostAsync<RegisterEmployeeInput, AuthApiResponse>("/api/Manager/Create/Employee", input);
        }
    }
}
