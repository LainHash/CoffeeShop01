using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.DTOs.Results;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class ManagerService : IManagerService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;

        public ManagerService(CoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ManagerResult> GetInfoAsync(Guid id)
        {
            var manager = await _context.Employees
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (manager == null)
            {
                return new ManagerResult(false, "Tài khoản không tồn tại!");
            }

            return new ManagerResult(true, "Lấy thông tin tài khoản thành công.", _mapper.Map<ManagerDTO>(manager));
        }

        public async Task<ManagerResult> LoginAsync(LoginDTO dto)
        {
            var manager = await _context.Employees
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.User.Email == dto.Email);

            if (manager == null)
            {
                return new ManagerResult(false, "Tài khoản không tồn tại!");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, manager.User.PasswordHash))
            {
                return new ManagerResult(false, "Email hoặc mật khẩu không đúng.");
            }

            return new ManagerResult(true, "Đăng nhập thành công.", _mapper.Map<ManagerDTO>(manager));
        }

        public async Task<ManagerResult> CreateEmployeeAsync(CreateEmployeeDTO dto)
        {
            var query = _context.Employees
                .Include(e => e.User);
            var existedEmail = await query.AnyAsync(e => e.User.Email == dto.Email);
            if (existedEmail) 
            {
                return new ManagerResult(false, "Email này đã được sử dụng.");
            }

            var existedUsername = await query.AnyAsync(e => e.User.Username == dto.Username);
            if (existedUsername)
            {
                return new ManagerResult(false, "Username này đã được sử dụng.");
            }

            var employee = _mapper.Map<Employee>(dto);
            employee.User = _mapper.Map<User>(dto);
            employee.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return new ManagerResult(true, "Tạo nhân viên thành công", _mapper.Map<ManagerDTO>(employee));
        }
    }
}
