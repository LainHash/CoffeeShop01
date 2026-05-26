using BlazorApp.Models.Orders;

namespace BlazorApp.Services.Interfaces
{
    public interface IOrderApiService
    {
        Task<OrderListResponse?> GetAllAsync();
        Task<OrderSingleResponse?> GetByIdAsync(Guid id);
        Task<OrderSingleResponse?> CreateAsync(CreateOrderInput input);
        // Note: For Checkout or status update, we can add it later if the API supports it.
        // E.g. Checkout might be PUT /api/Order/{id}/checkout
    }
}
