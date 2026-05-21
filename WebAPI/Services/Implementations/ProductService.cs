using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Products;
using WebAPI.DTOs.Products.Create;
using WebAPI.DTOs.Products.Update;
using WebAPI.DTOs.Results;
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

        public async Task<ProductResult> GetAllAsync()
        {
            var products = await _context.Products
                .Where(p => p.IsAvailable == true)
                .ToListAsync();
            return new ProductResult(true, "Lấy danh sách sản phẩm thành công.", _mapper.Map<List<ProductDTO>>(products));
        }

        public async Task<ProductResult> GetOneAsync(Guid id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.PublicId == id);
            if (product == null) 
            {
                return new ProductResult(false, "Sản phẩm không tồn tại!");
            }
            if (product.IsAvailable != true)
            {
                return new ProductResult(false, "Sản phẩm này đã bị xóa!");
            }
            return new ProductResult(true, "Lấy sản phẩm thành công", _mapper.Map<ProductDTO>(product));
        }

        public async Task<ProductResult> CreateAsync(CreateProductDTO dto)
        {
            var product = _mapper.Map<Product>(dto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ProductResult(true, "Tạo sản phẩm thành công.", _mapper.Map<ProductDTO>(product));
        }

        public async Task<ProductResult> UpdateAsync(Guid id, UpdateProductDTO dto)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.PublicId == id);
            if (product == null)
            {
                return new ProductResult(false, "Sản phẩm không tồn tại!");
            }
            if (product.IsAvailable != true)
            {
                return new ProductResult(false, "Sản phẩm này đã bị xóa!");
            }
            _mapper.Map(dto, product);
            await _context.SaveChangesAsync();
            return new ProductResult(true, "Cập nhật sản phẩm thành công.", _mapper.Map<ProductDTO>(product));
        }

        public async Task<ProductResult> DeleteAsync(Guid id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.PublicId == id);
            if(product == null)
            {
                return new ProductResult(false, "Sản phẩm không tồn tại!");
            }
            if(product.IsAvailable != true)
            {
                return new ProductResult(false, "Sản phẩm này đã bị xóa!");
            }
            product.IsAvailable = false;
            await _context.SaveChangesAsync();
            return new ProductResult(true, "Sản phẩm đã bị xóa thành công");
        }
    }
}
