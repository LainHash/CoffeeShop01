using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.Services.Implementations;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _managerService.LoginAsync(dto);
            if (!result.Success)
            {
                return BadRequest(new
                {
                    success = false,
                    message = result.Message
                });
            }
            return Ok(new
            {
                success = true,
                message = result.Message,
                manager = result.Manager
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfo(Guid id)
        {
            var result = await _managerService.GetInfoAsync(id);
            if (!result.Success)
            {
                return BadRequest(new
                {
                    success = false,
                    message = result.Message
                });
            }
            return Ok(new
            {
                success = true,
                message = result.Message,
                manager = result.Manager
            });
        }

        [HttpPost("Create/Employee")]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDTO dto)
        {
            var result = await _managerService.CreateEmployeeAsync(dto); if (!result.Success)
            {
                return BadRequest(new
                {
                    success = false,
                    message = result.Message,
                });
            }
            return Ok(new
            {
                success = true,
                message = result.Message,
                manager = result.Manager
            });

        }
    }
}
