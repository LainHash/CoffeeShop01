using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.ResultModels;

namespace WebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<ManagerResult<List<ManagerDTO>>> GetEmployeeListAsync();

        Task<CustomerResult<List<CustomerDTO>>> GetCustomerListAsync();
        
    }
}
