using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Accounts.Customers.Update;
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
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.User.Email == dto.Email);

            if (customer == null || customer.User.IsActive == false)
            {
                return new CustomerResult(false, "Tài khoản không tồn tại hoặc đã bị khóa!");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, customer.User.PasswordHash))
            {
                return new CustomerResult(false, "Email hoặc mật khẩu không đúng.");
            }

            return new CustomerResult(true, "Đăng nhập thành công.", _mapper.Map<CustomerDTO>(customer));
        }

        public async Task<CustomerResult> GetInfoAsync(Guid id)
        {
            var customer = await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (customer == null || customer.User.IsActive == false)
            {
                return new CustomerResult(false, "Tài khoản không tồn tại hoặc đã bị khóa!");
            }

            return new CustomerResult(true, "Lấy thông tin tài khoản thành công.", _mapper.Map<CustomerDTO>(customer));
        }

        public async Task<CustomerResult> RegisterAsync(RegisterDTO dto)
        {
            var query = _context.Customers
                .Include(c => c.User);
            var existedEmail = await query.AnyAsync(c => c.User.Email == dto.Email);
            if (existedEmail)
            {
                return new CustomerResult(false, "Email này đã được sử dụng.");
            }

            var existedUsername = await query.AnyAsync(c => c.User.Username == dto.Username);
            if (existedUsername)
            {
                return new CustomerResult(false, "Username này đã được sử dụng.");
            }

            var customer = _mapper.Map<Customer>(dto);
            customer.User = _mapper.Map<User>(dto);
            customer.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            customer.User.IsActive = true;
            
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new CustomerResult(true, "Đăng ký thành công.", _mapper.Map<CustomerDTO>(customer));
        }

        public async Task<CustomerResult> UpdateAsync(Guid id, UpdateInfoDTO dto)
        {
            var customer = await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (customer == null || customer.User.IsActive == false)
            {
                return new CustomerResult(false, "Tài khoản không tồn tại hoặc đã bị khóa!");
            }

            if (!string.IsNullOrEmpty(dto.Email) && dto.Email != customer.User.Email)
            {
                var existedEmail = await _context.Users.AnyAsync(u => u.Email == dto.Email);
                if (existedEmail)
                {
                    return new CustomerResult(false, "Email này đã được sử dụng.");
                }
                customer.User.Email = dto.Email;
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

            return new CustomerResult(true, "Cập nhật thông tin tài khoản thành công.", _mapper.Map<CustomerDTO>(customer));
        }

        public async Task<CustomerResult> DeleteAsync(Guid id)
        {
            var customer = await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (customer == null || customer.User.IsActive == false)
            {
                return new CustomerResult(false, "Khách hàng không tồn tại hoặc đã bị xóa trước đó!");
            }

            customer.User.IsActive = false;
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return new CustomerResult(true, "Xóa khách hàng thành công (Soft Delete).");
        }

        public async Task<CustomerResult> ChangePasswordAsync(Guid id, PasswordChangeDTO dto)
        {
            var customer = await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (customer == null || customer.User.IsActive == false)
            {
                return new CustomerResult(false, "Tài khoản không tồn tại hoặc đã bị khóa!");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, customer.User.PasswordHash))
            {
                return new CustomerResult(false, "Mật khẩu hiện tại không chính xác.");
            }

            if (dto.NewPassword != dto.ConfirmNewPassword)
            {
                return new CustomerResult(false, "Mật khẩu mới và xác nhận mật khẩu mới không khớp.");
            }

            customer.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return new CustomerResult(true, "Đổi mật khẩu thành công.", _mapper.Map<CustomerDTO>(customer));
        }
    }
}
