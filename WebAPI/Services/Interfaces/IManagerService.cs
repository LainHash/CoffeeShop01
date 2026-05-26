using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.DTOs.Accounts.Managers.Update;
using WebAPI.ResultModels;

namespace WebAPI.Services.Interfaces
{
    public interface IManagerService
    {
        Task<ManagerResult<ManagerDTO>> LoginAsync(LoginDTO dto);
        Task<ManagerResult<ManagerDTO>> GetInfoAsync(Guid id);
        Task<ManagerResult<ManagerDTO>> CreateEmployeeAsync(CreateEmployeeDTO dto);
        Task<ManagerResult<ManagerDTO>> UpdateAsync(Guid id, UpdateManagerInfoDTO dto);
        Task<ManagerResult> DeleteAsync(Guid id);
        Task<ManagerResult> ChangePasswordAsync(Guid id, PasswordChangeDTO dto);
    }
}
