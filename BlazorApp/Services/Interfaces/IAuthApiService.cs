using BlazorApp.Models.Auth;
using BlazorApp.Models.Managers;

namespace BlazorApp.Services.Interfaces
{
    public interface IAuthApiService
    {
        Task<AuthApiResponse?> LoginAsync(LoginInput input);
        Task<AuthApiResponse?> RegisterEmployeeAsync(RegisterEmployeeInput input);
    }
}
