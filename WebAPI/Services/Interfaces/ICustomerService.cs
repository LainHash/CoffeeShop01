using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResult> LoginAsync(LoginDTO dto);
        Task<CustomerResult> RegisterAsync(RegisterDTO dto);
        Task<CustomerResult> GetInfoAsync(Guid id);
    }
}
