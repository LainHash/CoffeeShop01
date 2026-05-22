using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Accounts.Customers.Update;
using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResult> GetAllAsync();

        Task<CustomerResult> GetInfoAsync(Guid id);

        Task<CustomerResult> UpdateAsync(Guid id, UpdateInfoDTO dto);

        Task<CustomerResult> DeleteAsync(Guid id);
    }
}
