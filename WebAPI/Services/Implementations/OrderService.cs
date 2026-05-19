using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Orders;
using WebAPI.DTOs.Orders.Create;
using WebAPI.DTOs.Results;
using WebAPI.Models;
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

        public async Task<OrderResult> CreateAsync(CreateOrderDTO request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var table = await _context.TableEntities.FindAsync(request.TableId);
                if (table == null)
                {
                    return new OrderResult(false, "Bàn không tồn tại.");
                }

                var employee = await _context.Employees.FindAsync(request.EmployeeId);
                if (employee == null)
                {
                    return new OrderResult(false, "Nhân viên không tồn tại.");
                }

                var order = _mapper.Map<Order>(request);
                order.PublicId = Guid.NewGuid();
                order.OrderTime = DateTime.UtcNow;
                order.CreatedAt = DateTime.UtcNow;

                decimal subTotal = 0;
                if (order.OrderDetails != null)
                {
                    foreach (var detail in order.OrderDetails)
                    {
                        var product = await _context.Products.FindAsync(detail.ProductId);
                        if (product == null)
                        {
                            return new OrderResult(false, $"Sản phẩm có ID {detail.ProductId} không tồn tại.");
                        }

                        detail.UnitPrice = product.Price;
                        detail.LineTotal = detail.Quantity * detail.UnitPrice;
                        subTotal += detail.LineTotal;
                    }
                }

                order.SubTotal = subTotal;
                
                decimal discountAmount = 0;
                if (order.DiscountId.HasValue)
                {
                    var discount = await _context.Discounts.FindAsync(order.DiscountId.Value);
                    if (discount != null)
                    {
                        if (discount.Type.ToLower() == "flat")
                        {
                            discountAmount = (decimal)discount.Value;
                        }
                        else if (discount.Type.ToLower() == "percent")
                        {
                            discountAmount = subTotal * (decimal)(discount.Value / 100.0);
                        }
                    }
                    else
                    {
                        return new OrderResult(false, "Mã giảm giá không tồn tại.");
                    }
                }

                order.TotalAmount = order.SubTotal - discountAmount;
                if (order.TotalAmount < 0) order.TotalAmount = 0;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var orderDto = _mapper.Map<OrderDTO>(order);
                return new OrderResult(true, "Tạo hóa đơn thành công", orderDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new OrderResult(false, $"Lỗi khi tạo hóa đơn: {ex.Message}");
            }
        }
    }
}
