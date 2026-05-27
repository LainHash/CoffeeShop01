using BlazorApp.Models.Products;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services.Implementations
{
    public class CategoryApiService : ICategoryApiService
    {
        private readonly IApiService _apiService;

        public CategoryApiService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<CategoryListResponse?> GetAllAsync()
        {
            return await _apiService.GetAsync<CategoryListResponse>("/api/Category");
        }
    }
}
