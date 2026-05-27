using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Discounts;
using WebAPI.ResultModels;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class DiscountService : IDiscountService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;

        public DiscountService(CoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DiscountResult<List<DiscountDTO>>> GetAllAsync()
        {
            var discounts = await _context.Discounts
                .ToListAsync();
            return new DiscountResult<List<DiscountDTO>>
            {
                Success = true,
                Message = "Lấy danh sách phiếu giảm giá thành công.",
                Data = _mapper.Map<List<DiscountDTO>>(discounts)
            };
        }

        public async Task<DiscountResult<DiscountDTO>> GetOneAsync(string code)
        {
            var discount = await _context.Discounts
                .FirstOrDefaultAsync(d => d.DiscountCode == code);
            if (discount == null)
            {
                return new DiscountResult<DiscountDTO>
                {
                    Success = false,
                    Message = "Phiếu giảm giá không tồn tại!",
                    Data = null
                };
            }
            return new DiscountResult<DiscountDTO>
            {
                Success = true,
                Message = "Lấy phiếu giảm giá thành công.",
                Data = _mapper.Map<DiscountDTO>(discount)
            };
        }
    }
}
