using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.DTOs.Accounts.Managers.Update;
using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface IManagerService
    {
        Task<ManagerResult> LoginAsync(LoginDTO dto);
        Task<ManagerResult> GetInfoAsync(Guid id);
        Task<ManagerResult> CreateEmployeeAsync(CreateEmployeeDTO dto);
        Task<ManagerResult> UpdateAsync(Guid id, UpdateManagerInfoDTO dto);
        Task<ManagerResult> DeleteAsync(Guid id);
        Task<ManagerResult> ChangePasswordAsync(Guid id, PasswordChangeDTO dto);
    }
}
