using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Discounts;
using WebAPI.DTOs.Results;
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

        public async Task<DiscountResult> GetAllAsync()
        {
            var discounts = await _context.Discounts
                .ToListAsync();
            return new DiscountResult(true, "Lấy danh sách phiếu giảm giá thành công.", _mapper.Map<List<DiscountDTO>>(discounts));
        }

        public async Task<DiscountResult> GetOneAsync(int id)
        {
            var discount = await _context.Discounts
                .FirstOrDefaultAsync(d => d.DiscountId == id);
            if (discount == null)
            {
                return new DiscountResult(false, "Phiếu giảm giá không tồn tại!");
            }
            return new DiscountResult(true, "Lấy phiếu giảm giá thành công.", _mapper.Map<DiscountDTO>(discount));
        }
    }
}
