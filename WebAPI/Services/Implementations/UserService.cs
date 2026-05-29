using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.ResultModels;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;

        public UserService(CoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerResult<CustomerDTO>> GetCustomerByIdAsync(Guid customerId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.PublicId == customerId);
            if(customer == null)
            {
                return new CustomerResult<CustomerDTO>
                {
                    Success = false,
                    Message = "Khách hàng không tồn tại."
                };
            }
            return new CustomerResult<CustomerDTO>
            {
                Success = true,
                Message = "Lấy thông tin khách hàng thành công.",
                Data = _mapper.Map<CustomerDTO>(customer)
            };
        }

        public async Task<CustomerResult<List<CustomerDTO>>> GetCustomerListAsync()
        {
            var customers = await _context.Customers
                .ToListAsync();
            return new CustomerResult<List<CustomerDTO>>
            {
                Success = true,
                Message = "Lấy danh sách khách hàng thành công.",
                Data = _mapper.Map<List<CustomerDTO>>(customers)
            };
        }

        public async Task<ManagerResult<ManagerDTO>> GetEmployeeByIdAsync(Guid employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.PublicId == employeeId);
            if(employee == null)
            {
                return new ManagerResult<ManagerDTO>
                {
                    Success = false,
                    Message = "Nhân viên không tồn tại."
                };
            }
            return new ManagerResult<ManagerDTO>
            {
                Success = true,
                Message = "Lấy thông tin nhân viên thành công.",
                Data = _mapper.Map<ManagerDTO>(employee)
            };
        }

        public async Task<ManagerResult<List<ManagerDTO>>> GetEmployeeListAsync()
        {
            var employees = await _context.Employees
                .Include(e => e.User).ToListAsync();
            return new ManagerResult<List<ManagerDTO>>
            {
                Success = true,
                Message = "Lấy danh sách nhân viên thành công.",
                Data = _mapper.Map<List<ManagerDTO>>(employees)
            };
        }

        public async Task<AccountResult> PasswordChangeAsync(Guid id, PasswordChangeDTO dto)
        {
            var manager = await _context.Employees
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.User.PublicId == id);

            if (manager == null || manager.User.IsActive == false)
            {
                return new AccountResult
                {
                    Success = false,
                    Message = "Tài khoản không tồn tại hoặc đã bị khóa!"
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, manager.User.PasswordHash))
            {
                return new AccountResult
                {
                    Success = false,
                    Message = "Mật khẩu hiện tại không chính xác."
                };
            }

            if (dto.NewPassword != dto.ConfirmNewPassword)
            {
                return new AccountResult
                {
                    Success = false,
                    Message = "Mật khẩu mới và xác nhận mật khẩu mới không khớp."
                };
            }

            manager.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            _context.Employees.Update(manager);
            await _context.SaveChangesAsync();

            return new AccountResult
            {
                Success = true,
                Message = "Đổi mật khẩu thành công."
            };
        }
    }
}
