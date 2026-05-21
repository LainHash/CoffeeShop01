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
                .Include(o => o.Table)
                .Include(o => o.Employee)
                .Include(od => od.OrderDetails)
                    .ThenInclude(od => od.Product)
                .ToListAsync();

            var orderDtos = _mapper.Map<List<OrderDTO>>(orders);
            foreach (var dto in orderDtos)
            {
                if (dto.DiscountId.HasValue && dto.DiscountId.Value > 0)
                {
                    var discount = await _context.Discounts.FindAsync(dto.DiscountId.Value);
                    if (discount != null) dto.DiscountCode = discount.DiscountCode;
                }
            }

            return new OrderResult(true, "Lấy danh sách hóa đơn thành công", orderDtos);
        }

        public async Task<OrderResult> GetOneAsync(Guid id)
        {
            var order = await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.Employee)
                .Include(od => od.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(od => od.PublicId == id);
            if (order == null)
            {
                return new OrderResult(false, "Hóa đơn không tồn tại!");
            }

            var orderDto = _mapper.Map<OrderDTO>(order);
            if (order.DiscountId.HasValue && order.DiscountId.Value > 0)
            {
                var discount = await _context.Discounts.FindAsync(order.DiscountId.Value);
                if (discount != null) orderDto.DiscountCode = discount.DiscountCode;
            }

            return new OrderResult(true, "Lấy hóa đơn thành công", orderDto);
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
                        if (discount.Type.Equals(DiscountType.Flat, StringComparison.OrdinalIgnoreCase))
                        {
                            discountAmount = (decimal)discount.Value;
                        }
                        else if (discount.Type.Equals(DiscountType.Percent, StringComparison.OrdinalIgnoreCase))
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

        public async Task<OrderResult> Checkout(Guid id, bool confirm, string paymentMethod = "Cash", string? note = null)
        {
            var order = await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.Employee)
                .Include(od => od.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(od => od.PublicId == id);

            if (order == null)
            {
                return new OrderResult(false, "Hóa đơn không tồn tại!");
            }

            if (confirm)
            {
                order.Status = InvoiceStatuses.Paid;
                
                var payment = new Payment
                {
                    OrderId = order.OrderId,
                    PaymentTime = DateTime.Now,
                    PaymentMethod = paymentMethod,
                    PaidAmount = order.TotalAmount,
                    Note = note ?? "Thanh toán hóa đơn"
                };
                _context.Payments.Add(payment);
            }
            else
            {
                order.Status = InvoiceStatuses.Cancelled;
            }

            await _context.SaveChangesAsync();

            var orderDto = _mapper.Map<OrderDTO>(order);
            if (order.DiscountId.HasValue && order.DiscountId.Value > 0)
            {
                var discount = await _context.Discounts.FindAsync(order.DiscountId.Value);
                if (discount != null) orderDto.DiscountCode = discount.DiscountCode;
            }

            return new OrderResult(
                true,
                confirm ? "Thanh toán hóa đơn thành công" : "Hóa đơn đã bị hủy",
                orderDto
            );
        }
    }
}
