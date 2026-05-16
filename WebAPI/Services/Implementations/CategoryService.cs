using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Categories;
using WebAPI.DTOs.Results;
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

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _context.Categories
                .ToListAsync();
            var dtos = _mapper.Map<List<CategoryDTO>>(categories);
            return dtos;
        }
    }
}
