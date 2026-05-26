using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Categories;
using WebAPI.ResultModels;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(CoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryResult<List<CategoryDTO>>> GetAllAsync()
        {
            var categories = await _context.Categories
                .ToListAsync();
            return new CategoryResult<List<CategoryDTO>>
            {
                Success = true,
                Message = "Lấy danh sách danh mục thành công.",
                Data = _mapper.Map<List<CategoryDTO>>(categories)
            };
        }
    }
}
