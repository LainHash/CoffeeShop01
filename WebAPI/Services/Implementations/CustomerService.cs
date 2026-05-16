using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Results;
using WebAPI.Models;
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

        public async Task<CustomerResult> LoginAsync(LoginDTO dto)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == dto.Email);

            if (customer == null)
            {
                return new CustomerResult(false, "Tài khoản không tồn tại!");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, customer.PasswordHash))
            {
                return new CustomerResult(false, "Email hoặc mật khẩu không đúng.");
            }

            // Map thông tin customer để trả về cho client
            var customerDto = _mapper.Map<CustomerDTO>(customer);
            return new CustomerResult(true, "Đăng nhập thành công.", customerDto);
        }

        public async Task<CustomerResult> RegisterAsync(RegisterDTO dto)
        {
            var existedEmail = await _context.Customers.AnyAsync(c => c.Email == dto.Email);
            if (existedEmail)
            {
                return new CustomerResult(false, "Email này đã được sử dụng.");
            }

            var existedUsername = await _context.Customers.AnyAsync(c => c.Username == dto.Username);
            if (existedUsername)
            {
                return new CustomerResult(false, "Username này đã được sử dụng.");
            }

            var customer = _mapper.Map<Customer>(dto);
            customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new CustomerResult(true, "Đăng ký thành công.");
        }
    }
}
