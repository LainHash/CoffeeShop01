using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Accounts.Customers.Update;
using WebAPI.ResultModels;

namespace WebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResult<List<CustomerDTO>>> GetAllAsync();

        Task<CustomerResult<CustomerDTO>> GetInfoAsync(Guid id);

        Task<CustomerResult<CustomerDTO>> UpdateAsync(Guid id, UpdateInfoDTO dto);

        Task<CustomerResult> DeleteAsync(Guid id);
    }
}
