using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.DTOs.Accounts.Managers.Update;
using WebAPI.ResultModels;

namespace WebAPI.Services.Interfaces
{
    public interface IManagerService
    {
        Task<ManagerResult<ManagerDTO>> CreateEmployeeAsync(CreateEmployeeDTO dto);
    }
}
