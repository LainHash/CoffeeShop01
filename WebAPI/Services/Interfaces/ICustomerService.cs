using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Customers.Update;
using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResult> LoginAsync(LoginDTO dto);
        Task<CustomerResult> RegisterAsync(RegisterDTO dto);
        Task<CustomerResult> GetInfoAsync(Guid id);
        Task<CustomerResult> UpdateAsync(Guid id, UpdateInfoDTO dto);
        Task<CustomerResult> DeleteAsync(Guid id);
        Task<CustomerResult> ChangePasswordAsync(Guid id, PasswordChangeDTO dto);
    }
}
