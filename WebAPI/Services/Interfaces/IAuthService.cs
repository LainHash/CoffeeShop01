using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.ResultModels;

namespace WebAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult<ManagerDTO>> LoginAsync(LoginDTO dto);
    }
}
