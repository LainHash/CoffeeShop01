using BlazorApp.Models.Products;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services.Implementations
{
    public class ProductApiService : IProductApiService
    {
        private readonly IApiService _apiService;

        public ProductApiService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ProductListResponse?> GetAllAsync()
        {
            return await _apiService.GetAsync<ProductListResponse>("/api/Product");
        }

        public async Task<ProductResponse?> GetByIdAsync(Guid id)
        {
            return await _apiService.GetAsync<ProductResponse>($"/api/Product/{id}");
        }

        public async Task<ProductResponse?> CreateAsync(CreateProductInput input)
        {
            return await _apiService.PostAsync<CreateProductInput, ProductResponse>("/api/Product", input);
        }

        public async Task<ProductResponse?> UpdateAsync(Guid id, UpdateProductInput input)
        {
            return await _apiService.PutAsync<UpdateProductInput, ProductResponse>($"/api/Product/{id}", input);
        }

        public async Task<ProductResponse?> DeleteAsync(Guid id)
        {
            return await _apiService.DeleteAsync<ProductResponse>($"/api/Product/{id}");
        }
    }
}
