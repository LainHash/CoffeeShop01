using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Accounts.Managers;
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
            employee.User.RoleId = 2;
            
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return new ManagerResult<ManagerDTO>
            {
                Success = true,
                Message = "Tạo nhân viên thành công",
                Data = _mapper.Map<ManagerDTO>(employee)
            };
        }
    }
}
