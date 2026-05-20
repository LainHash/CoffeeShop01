using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Orders;
using WebAPI.DTOs.Orders.Create;
using WebAPI.DTOs.Orders.Update;
using WebAPI.DTOs.Results;
using WebAPI.Helpers.Constants.Orders;
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
                var table = await _context.TableEntities
                    .FirstOrDefaultAsync(t => t.TableId == request.TableId);
                if (table == null)
                {
                    return new OrderResult(false, "Bàn không tồn tại.");
                }

                var employee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.PublicId == request.EmployeePublicId);
                if (employee == null)
                {
                    return new OrderResult(false, "Nhân viên không tồn tại.");
                }

                var order = _mapper.Map<Order>(request);
                order.EmployeeId = employee.EmployeeId;

                decimal subTotal = 0;
                if (order.OrderDetails != null)
                {
                    foreach (var detail in order.OrderDetails)
                    {
                        var product = await _context.Products
                            .FirstOrDefaultAsync(p => p.ProductId == detail.ProductId);
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
                        if (discount.Type.ToLower() == DiscountType.Flat)
                        {
                            discountAmount = (decimal)discount.Value;
                        }
                        else if (discount.Type.ToLower() == DiscountType.Percent)
                        {
                            discountAmount = subTotal * (decimal)(discount.Value);
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

        public Task<OrderResult> UpdateAsync(Guid id, UpdateOrderDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderResult> Checkout(Guid id, UpdateOrderDTO request, bool confirm)
        {
            var order = await _context.Orders
                .Include(od => od.OrderDetails)
                .FirstOrDefaultAsync(od => od.PublicId == id);

            if (order == null)
            {
                return new OrderResult(false, "Hóa đơn không tồn tại!");
            }

            return new OrderResult(
                confirm,
                confirm ? "Thanh toán hóa đơn thành công" : "Hóa đơn đã bị hủy",
                _mapper.Map<OrderDTO>(order)
            );
        }
    }
}
