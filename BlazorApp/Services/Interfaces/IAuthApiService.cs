using BlazorApp.Models.Auth;

namespace BlazorApp.Services.Interfaces
{
    public interface IAuthApiService
    {
        Task<AuthApiResponse?> LoginAsync(LoginInput input);
        Task<AuthApiResponse?> RegisterEmployeeAsync(RegisterEmployeeInput input);
    }
}
