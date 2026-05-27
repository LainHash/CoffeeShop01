using BlazorApp.Models.Orders;

namespace BlazorApp.Services.Interfaces
{
    public interface IOrderApiService
    {
        Task<OrderListResponse?> GetAllAsync();
        Task<OrderSingleResponse?> GetByIdAsync(Guid id);
        Task<OrderSingleResponse?> CreateAsync(CreateOrderInput input);
        Task<OrderSingleResponse?> CheckoutAsync(Guid id, string paymentMethod, string? note);
    }
}
