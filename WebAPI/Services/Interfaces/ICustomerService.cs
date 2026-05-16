using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResult> LoginAsync(LoginDTO dto);
        Task<CustomerResult> RegisterAsync(RegisterDTO dto);
    }
}
