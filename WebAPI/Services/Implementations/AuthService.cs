using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.ResultModels;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;

        public AuthService(CoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ManagerResult<ManagerDTO>> LoginAsync(LoginDTO dto)
        {
            var employee = await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.User.Email == dto.Email);
            if(employee == null)
            {
                return new ManagerResult<ManagerDTO>
                {
                    Success = false,
                    Message = "Thông tin nhân viên không tồn tại."
                };
            }
            if (employee.User == null || employee.User.IsActive == false)
            {
                return new ManagerResult<ManagerDTO>
                {
                    Success = false,
                    Message = "Tài khoản không tồn tại hoặc đã bị khóa."
                };
            }
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, employee.User.PasswordHash))
            {
                return new ManagerResult<ManagerDTO>
                {
                    Success = false,
                    Message = "Email hoặc mật khẩu không đúng."
                };
            }

            return new ManagerResult<ManagerDTO>
            {
                Success = true,
                Message = "Đăng nhập thành công.",
                Data = _mapper.Map<ManagerDTO>(employee)
            };
        }
    }
}
