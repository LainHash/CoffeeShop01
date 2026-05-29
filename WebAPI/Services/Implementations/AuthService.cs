using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly IConfiguration _config;

        public AuthService(CoffeeShopDbContext context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        public async Task<AuthResult<ManagerDTO>> LoginAsync(LoginDTO dto)
        {
            var employee = await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.User.Email == dto.Email);
            if (employee == null)
            {
                return new AuthResult<ManagerDTO>
                {
                    Success = false,
                    Message = "Thông tin nhân viên không tồn tại."
                };
            }
            if (employee.User == null || employee.User.IsActive == false)
            {
                return new AuthResult<ManagerDTO>
                {
                    Success = false,
                    Message = "Tài khoản không tồn tại hoặc đã bị khóa."
                };
            }
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, employee.User.PasswordHash))
            {
                return new AuthResult<ManagerDTO>
                {
                    Success = false,
                    Message = "Email hoặc mật khẩu không đúng."
                };
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employee.User.PublicId.ToString()),
                new Claim(ClaimTypes.Email, employee.User.Email),
                new Claim(ClaimTypes.Name, employee.FullName),
                new Claim(ClaimTypes.Role, employee.User.RoleId.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResult<ManagerDTO>
            {
                Success = true,
                Message = "Đăng nhập thành công.",
                Data = _mapper.Map<ManagerDTO>(employee),
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
