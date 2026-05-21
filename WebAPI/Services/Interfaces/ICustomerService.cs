using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Accounts.Customers.Update;
using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>Lấy tất cả khách hàng</summary>
        Task<CustomerResult> GetAllAsync();

        /// <summary>Lấy thông tin một khách hàng theo PublicId</summary>
        Task<CustomerResult> GetInfoAsync(Guid id);

        /// <summary>Cập nhật thông tin khách hàng</summary>
        Task<CustomerResult> UpdateAsync(Guid id, UpdateInfoDTO dto);

        /// <summary>Xoá khách hàng (soft delete)</summary>
        Task<CustomerResult> DeleteAsync(Guid id);
    }
}
