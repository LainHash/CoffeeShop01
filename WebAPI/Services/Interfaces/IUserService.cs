using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.ResultModels;

namespace WebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<ManagerResult<List<ManagerDTO>>> GetEmployeeListAsync();
        Task<ManagerResult<ManagerDTO>> GetEmployeeByIdAsync(Guid employeeId);

        Task<CustomerResult<List<CustomerDTO>>> GetCustomerListAsync();
        Task<CustomerResult<CustomerDTO>> GetCustomerByIdAsync(Guid customerId);

        Task<AccountResult> PasswordChangeAsync(Guid userId, PasswordChangeDTO dto);
        
    }
}
