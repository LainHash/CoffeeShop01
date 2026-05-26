using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Accounts.Customers.Update;
using WebAPI.ResultModels;
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

        public async Task<CustomerResult<List<CustomerDTO>>> GetAllAsync()
        {
            var customers = await _context.Customers
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return new CustomerResult<List<CustomerDTO>>
            {
                Success = true,
                Message = "Lấy danh sách khách hàng thành công.",
                Data = _mapper.Map<List<CustomerDTO>>(customers)
            };
        }

        public async Task<CustomerResult<CustomerDTO>> GetInfoAsync(Guid id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (customer == null)
                return new CustomerResult<CustomerDTO>
                {
                    Success = false,
                    Message = "Khách hàng không tồn tại."
                };

            return new CustomerResult<CustomerDTO>
            {
                Success = true,
                Message = "Lấy thông tin khách hàng thành công.",
                Data = _mapper.Map<CustomerDTO>(customer)
            };
        }

        public async Task<CustomerResult<CustomerDTO>> UpdateAsync(Guid id, UpdateInfoDTO dto)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (customer == null)
            {
                return new CustomerResult<CustomerDTO>
                {
                    Success = false,
                    Message = "Khách hàng không tồn tại."
                };
            }

            if (!string.IsNullOrEmpty(dto.FullName))
            {
                customer.FullName = dto.FullName;
            }

            if (!string.IsNullOrEmpty(dto.Phone))
            {
                customer.Phone = dto.Phone;
            }

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return new CustomerResult<CustomerDTO>
            {
                Success = true,
                Message = "Cập nhật thông tin khách hàng thành công.",
                Data = _mapper.Map<CustomerDTO>(customer)
            };
        }

        public async Task<CustomerResult> DeleteAsync(Guid id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (customer == null)
                return new CustomerResult
                {
                    Success = false,
                    Message = "Khách hàng không tồn tại."
                };

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return new CustomerResult
            {
                Success = true,
                Message = "Xoá khách hàng thành công."
            };
        }
    }
}
