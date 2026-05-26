using BlazorApp.Models.Discounts;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services.Implementations
{
    public class DiscountApiService : IDiscountApiService
    {
        private readonly IApiService _apiService;

        public DiscountApiService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<DiscountListResponse?> GetAllAsync()
        {
            return await _apiService.GetAsync<DiscountListResponse>("/api/Discount");
        }
    }
}
