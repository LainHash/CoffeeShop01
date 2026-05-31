using WebAPI.DTOs.Products;
using WebAPI.DTOs.Products.Create;
using WebAPI.DTOs.Products.Update;
using WebAPI.ResultModels;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductResult<List<ProductDTO>>> GetAllAsync();

        Task<ProductResult<ProductDTO>> CreateAsync(CreateProductDTO dto);
        Task<ProductResult<ProductDTO>> UpdateAsync(Guid id, UpdateProductDTO dto);

        Task<ProductResult> DeleteAsync(Guid id);
    }
}
