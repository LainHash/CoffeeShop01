using WebAPI.ResultModels;
using WebAPI.DTOs.Orders.Create;
using WebAPI.DTOs.Orders.Update;
using WebAPI.DTOs.Orders;

namespace WebAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResult<List<OrderDTO>>> GetAllAsync();
        Task<OrderResult<OrderDTO>> GetOneAsync(Guid id);

        Task<OrderResult<OrderDTO>> CreateAsync(CreateOrderDTO request);
        Task<OrderResult<OrderDTO>> UpdateAsync(Guid id, UpdateOrderDTO request);

        Task<OrderResult<OrderDTO>> Checkout(Guid id, bool confirm, string paymentMethod = "Cash", string? note = null);

    }
}
