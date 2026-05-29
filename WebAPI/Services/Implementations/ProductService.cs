using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Products;
using WebAPI.DTOs.Products.Create;
using WebAPI.DTOs.Products.Update;
using WebAPI.ResultModels;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(CoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductResult<List<ProductDTO>>> GetAllAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsAvailable == true)
                .ToListAsync();
            return new ProductResult<List<ProductDTO>>
            {
                Success = true,
                Message = "Lấy danh sách sản phẩm thành công.",
                Data = _mapper.Map<List<ProductDTO>>(products)
            };
        }

        public async Task<ProductResult<ProductDTO>> GetOneAsync(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.PublicId == id);
            if (product == null)
            {
                return new ProductResult<ProductDTO>
                {
                    Success = false,
                    Message = "Sản phẩm không tồn tại!"
                };
            }
            if (product.IsAvailable != true)
            {
                return new ProductResult<ProductDTO>
                {
                    Success = false,
                    Message = "Sản phẩm này đã bị xóa!"
                };
            }
            return new ProductResult<ProductDTO>
            {
                Success = true,
                Message = "Lấy sản phẩm thành công",
                Data = _mapper.Map<ProductDTO>(product)
            };
        }

        public async Task<ProductResult<ProductDTO>> CreateAsync(CreateProductDTO dto)
        {
            var product = _mapper.Map<Product>(dto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ProductResult<ProductDTO>
            {
                Success = true,
                Message = "Tạo sản phẩm thành công.",
                Data = _mapper.Map<ProductDTO>(product)
            };
        }

        public async Task<ProductResult<ProductDTO>> UpdateAsync(Guid id, UpdateProductDTO dto)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.PublicId == id);
            if (product == null)
            {
                return new ProductResult<ProductDTO>
                {
                    Success = false,
                    Message = "Sản phẩm không tồn tại!"
                };
            }
            if (product.IsAvailable != true)
            {
                return new ProductResult<ProductDTO>
                {
                    Success = false,
                    Message = "Sản phẩm này đã bị xóa!"
                };
            }
            _mapper.Map(dto, product);
            await _context.SaveChangesAsync();
            return new ProductResult<ProductDTO>
            {
                Success = true,
                Message = "Cập nhật sản phẩm thành công.",
                Data = _mapper.Map<ProductDTO>(product)
            };
        }

        public async Task<ProductResult> DeleteAsync(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.PublicId == id);
            if (product == null)
            {
                return new ProductResult
                {
                    Success = false,
                    Message = "Sản phẩm không tồn tại!"
                };
            }
            if (product.IsAvailable != true)
            {
                return new ProductResult
                {
                    Success = false,
                    Message = "Sản phẩm này đã bị xóa!"
                };
            }
            product.IsAvailable = false;
            await _context.SaveChangesAsync();
            return new ProductResult
            {
                Success = true,
                Message = "Sản phẩm đã bị xóa thành công"
            };
        }
    }
}
