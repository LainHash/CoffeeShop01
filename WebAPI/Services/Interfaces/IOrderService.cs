using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResult> GetAllAsync();
        Task<OrderResult> GetOneAsync(Guid id);
    }
}
