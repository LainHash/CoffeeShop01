using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Accounts.Customers.Update;
using WebAPI.DTOs.Results;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(CoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerResult> GetAllAsync()
        {
            var customers = await _context.Customers
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return new CustomerResult(
                true,
                "Lấy danh sách khách hàng thành công.",
                customers: _mapper.Map<List<CustomerDTO>>(customers));
        }

        public async Task<CustomerResult> GetInfoAsync(Guid id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (customer == null)
                return new CustomerResult(false, "Khách hàng không tồn tại.");

            return new CustomerResult(true, "Lấy thông tin khách hàng thành công.",
                _mapper.Map<CustomerDTO>(customer));
        }

        public async Task<CustomerResult> UpdateAsync(Guid id, UpdateInfoDTO dto)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (customer == null)
                return new CustomerResult(false, "Khách hàng không tồn tại.");

            if (!string.IsNullOrEmpty(dto.FullName))
                customer.FullName = dto.FullName;

            if (!string.IsNullOrEmpty(dto.Phone))
            {
                var phoneExists = await _context.Customers
                    .AnyAsync(c => c.Phone == dto.Phone && c.PublicId != id);
                if (phoneExists)
                    return new CustomerResult(false, "Số điện thoại này đã được sử dụng bởi khách hàng khác.");

                customer.Phone = dto.Phone;
            }

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return new CustomerResult(true, "Cập nhật thông tin khách hàng thành công.",
                _mapper.Map<CustomerDTO>(customer));
        }

        public async Task<CustomerResult> DeleteAsync(Guid id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (customer == null)
                return new CustomerResult(false, "Khách hàng không tồn tại.");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return new CustomerResult(false, "Xoá khách hàng thành công.");
        }
    }
}
