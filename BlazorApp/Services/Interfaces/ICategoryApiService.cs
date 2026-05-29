using BlazorApp.Models.Products;

namespace BlazorApp.Services.Interfaces
{
    public interface ICategoryApiService
    {
        Task<CategoryListResponse?> GetAllAsync();
    }
}
