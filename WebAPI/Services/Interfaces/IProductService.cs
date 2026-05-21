using WebAPI.DTOs.Products;
using WebAPI.DTOs.Products.Create;
using WebAPI.DTOs.Products.Update;
using WebAPI.DTOs.Results;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductResult> GetAllAsync();
        Task<ProductResult> GetOneAsync(Guid id);

        Task<ProductResult> CreateAsync(CreateProductDTO dto);
        Task<ProductResult> UpdateAsync(Guid id, UpdateProductDTO dto);

        Task<ProductResult> DeleteAsync(Guid id);
    }
}
