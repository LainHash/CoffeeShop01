using WebAPI.DTOs.Results;
using WebAPI.DTOs.Orders.Create;

namespace WebAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResult> GetAllAsync();
        Task<OrderResult> GetOneAsync(Guid id);
        Task<OrderResult> CreateAsync(CreateOrderDTO request);
    }
}
