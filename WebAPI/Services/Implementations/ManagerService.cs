using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.DTOs.Accounts.Managers.Update;
using WebAPI.ResultModels;
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

        public async Task<ManagerResult<ManagerDTO>> GetInfoAsync(Guid id)
        {
            var manager = await _context.Employees
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (manager == null || manager.User.IsActive == false)
            {
                return new ManagerResult<ManagerDTO>
                {
                    Success = false,
                    Message = "Tài khoản không tồn tại hoặc đã bị khóa!",
                    Data = null
                };
            }

            return new ManagerResult<ManagerDTO>
            {
                Success = true,
                Message = "Lấy thông tin tài khoản thành công.",
                Data = _mapper.Map<ManagerDTO>(manager)
            };
        }

        public async Task<ManagerResult<ManagerDTO>> CreateEmployeeAsync(CreateEmployeeDTO dto)
        {
            var query = _context.Employees
                .Include(e => e.User);
            var existedEmail = await query.AnyAsync(e => e.User.Email == dto.Email);
            if (existedEmail) 
            {
                return new ManagerResult<ManagerDTO>
                {
                    Success = false,
                    Message = "Email này đã được sử dụng."
                };
            }

            var existedUsername = await query.AnyAsync(e => e.User.Username == dto.Username);
            if (existedUsername)
            {
                return new ManagerResult<ManagerDTO>
                {
                    Success = false,
                    Message = "Username này đã được sử dụng."
                };
            }

            var employee = _mapper.Map<Employee>(dto);
            employee.User = _mapper.Map<User>(dto);
            employee.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            employee.User.IsActive = true;
            
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return new ManagerResult<ManagerDTO>
            {
                Success = true,
                Message = "Tạo nhân viên thành công",
                Data = _mapper.Map<ManagerDTO>(employee)
            };
        }

        public async Task<ManagerResult> DeleteAsync(Guid id)
        {
            var manager = await _context.Employees
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (manager == null || manager.User.IsActive == false)
            {
                return new ManagerResult
                {
                    Success = false,
                    Message = "Tài khoản không tồn tại hoặc đã bị xóa trước đó!"
                };
            }

            manager.User.IsActive = false;
            _context.Employees.Update(manager);
            await _context.SaveChangesAsync();

            return new ManagerResult
            {
                Success = true,
                Message = "Xóa tài khoản quản lý/nhân viên thành công (Soft Delete)."
            };
        }

        public async Task<ManagerResult> ChangePasswordAsync(Guid id, PasswordChangeDTO dto)
        {
            var manager = await _context.Employees
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (manager == null || manager.User.IsActive == false)
            {
                return new ManagerResult
                {
                    Success = false,
                    Message = "Tài khoản không tồn tại hoặc đã bị khóa!"
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, manager.User.PasswordHash))
            {
                return new ManagerResult
                {
                    Success = false,
                    Message = "Mật khẩu hiện tại không chính xác."
                };
            }

            if (dto.NewPassword != dto.ConfirmNewPassword)
            {
                return new ManagerResult
                {
                    Success = false,
                    Message = "Mật khẩu mới và xác nhận mật khẩu mới không khớp."
                };
            }

            manager.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            _context.Employees.Update(manager);
            await _context.SaveChangesAsync();

            return new ManagerResult
            {
                Success = true,
                Message = "Đổi mật khẩu thành công."
            };
        }
    }
}
