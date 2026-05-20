using WebAPI.DTOs.Results;
using WebAPI.DTOs.Orders.Create;
using WebAPI.DTOs.Orders.Update;

namespace WebAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResult> GetAllAsync();
        Task<OrderResult> GetOneAsync(Guid id);

        Task<OrderResult> CreateAsync(CreateOrderDTO request);
        Task<OrderResult> UpdateAsync(Guid id, UpdateOrderDTO request);

        Task<OrderResult> Checkout(Guid id, UpdateOrderDTO request, bool isPaid);

    }
}
