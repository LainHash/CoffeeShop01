using WebAPI.DTOs.Products;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface IProductsService
    {
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<ProductResult> GetOneProductAsync(Guid id);

        Task<ProductResult> CreateProductAsync(CreateProductDTO dto);
        Task<ProductResult> UpdateProductAsync(Guid id, UpdateProductDTO dto);

        Task<ProductResult> DeleteProductAsync(Guid id);
    }
}
