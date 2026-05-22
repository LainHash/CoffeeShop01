using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Accounts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CoffeeShopDbContext _context;

        public AuthController(CoffeeShopDbContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var user = await _context.Users
                .Include(u => u.Employee)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || user.IsActive == false)
            {
                return BadRequest(new { success = false, message = "Tài khoản không tồn tại hoặc đã bị khóa!" });
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return BadRequest(new { success = false, message = "Email hoặc mật khẩu không đúng." });
            }

            if (user.RoleId == 1) 
            {
                return BadRequest(new { success = false, message = "Hệ thống chỉ dành cho nhân viên." });
            }

            if (user.Employee == null)
            {
                return BadRequest(new { success = false, message = "Lỗi dữ liệu nhân viên." });
            }

            return Ok(new
            {
                success = true,
                message = "Đăng nhập thành công.",
                roleId = user.RoleId,
                manager = new
                {
                    user.Employee.PublicId,
                    user.Employee.FullName,
                    user.Employee.Phone,
                    user.Employee.Position,
                    user.Email,
                    user.Username
                }
            });
        }
    }
}
