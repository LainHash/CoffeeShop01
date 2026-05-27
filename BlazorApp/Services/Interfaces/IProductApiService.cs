using BlazorApp.Models.Products;

namespace BlazorApp.Services.Interfaces
{
    public interface IProductApiService
    {
        Task<ProductListResponse?> GetAllAsync();
        Task<ProductResponse?> GetByIdAsync(Guid id);
        Task<ProductResponse?> CreateAsync(CreateProductInput input);
        Task<ProductResponse?> UpdateAsync(Guid id, UpdateProductInput input);
        Task<ProductResponse?> DeleteAsync(Guid id);
    }
}
