using BlazorApp.Models.Orders;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services.Implementations
{
    public class OrderApiService : IOrderApiService
    {
        private readonly IApiService _apiService;

        public OrderApiService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<OrderListResponse?> GetAllAsync()
        {
            return await _apiService.GetAsync<OrderListResponse>("/api/Order");
        }

        public async Task<OrderSingleResponse?> GetByIdAsync(Guid id)
        {
            return await _apiService.GetAsync<OrderSingleResponse>($"/api/Order/{id}");
        }

        public async Task<OrderSingleResponse?> CreateAsync(CreateOrderInput input)
        {
            return await _apiService.PostAsync<CreateOrderInput, OrderSingleResponse>("/api/Order", input);
        }

        public async Task<OrderSingleResponse?> CheckoutAsync(Guid id, string paymentMethod, string? note)
        {
            var endpoint = $"/api/Order/{id}/Checkout?confirm=true&paymentMethod={Uri.EscapeDataString(paymentMethod)}&note={Uri.EscapeDataString(note ?? "")}";
            return await _apiService.PostAsync<object, OrderSingleResponse>(endpoint, new { });
        }
    }
}
