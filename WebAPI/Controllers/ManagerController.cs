using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Accounts;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.DTOs.Accounts.Managers.Update;
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

        [HttpPost("Create/Employee")]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDTO dto)
        {
            var result = await _managerService.CreateEmployeeAsync(dto);
            if (!result.Success)
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
                manager = result.Data
            });
        }

    }
}
