using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Orders;
using WebAPI.DTOs.Results;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(CoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderResult> GetAllAsync()
        {
            var orders = await _context.Orders
                .Include(od => od.OrderDetails)
                .ToListAsync();
            return new OrderResult(true, "Lấy danh sách hóa đơn thành công", _mapper.Map<List<OrderDTO>>(orders));
        }

        public async Task<OrderResult> GetOneAsync(Guid id)
        {
            var order = await _context.Orders
                .Include(od => od.OrderDetails)
                .FirstOrDefaultAsync(od => od.PublicId == id);
            if (order == null) 
            {
                return new OrderResult(false, "Hóa đơn không tồn tại!");
            }

            return new OrderResult(true, "Lấy hóa đơn thành công", _mapper.Map<OrderDTO>(order));
        }
    }
}
