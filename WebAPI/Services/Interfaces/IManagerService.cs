using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface IManagerService
    {
        Task<ManagerResult> LoginAsync(LoginDTO dto);
        Task<ManagerResult> GetInfoAsync(Guid id);

        Task<ManagerResult> CreateEmployeeAsync(CreateEmployeeDTO dto);
    }
}
