using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Results;
using WebAPI.Helpers.Constants.Sessions;
using WebAPI.Helpers.Extensions;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerService(CoffeeShopDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CustomerResult> LoginAsync(LoginDTO dto)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == dto.Email);
            if (customer == null)
            {
                return new CustomerResult(false, "Tài khoản không tồn tại!");
            }

            if (customer != null && BCrypt.Net.BCrypt.Verify(dto.Password, customer.PasswordHash))
            {
                _httpContextAccessor.HttpContext?.Session
                    .SetString(AccountConstants.CustomerId, customer.PublicId.ToString());
                _httpContextAccessor.HttpContext?.Session
                    .SetString(AccountConstants.Username, customer.Username);
                _httpContextAccessor.HttpContext?.Session
                    .SetString(AccountConstants.Email, customer.Email);

                return new CustomerResult(true, "Đăng nhập thành công.");
            }
            return new CustomerResult(false, "Email hoặc mật khẩu không đúng.");
        }

        public async Task<CustomerResult> LogoutAsync()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
            return new CustomerResult(true, "Đăng xuất thành công.");
        }

        public async Task<CustomerResult> RegisterAsync(RegisterDTO dto)
        {
            var existedEmail = await _context.Customers.AnyAsync(c => c.Email == dto.Email);
            if (existedEmail)
            {
                return new CustomerResult(false, "Email này đã được sử dụng.");
            }
            var customer = _mapper.Map<Customer>(dto);
            customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return new CustomerResult(true, "Đăng ký thành công.");
        }
    }
}
