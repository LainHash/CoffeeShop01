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
    }
}
