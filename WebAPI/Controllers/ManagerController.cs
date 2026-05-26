using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.DTOs.Accounts.Managers.Update;
using WebAPI.Helpers.Extensions;
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
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDTO dto,
            [FromServices] IValidator<CreateEmployeeDTO> validator)
        {
            var error = await validator.ValidateAndReturnError(dto);
            if (error != null)
            {
                return error;
            }

            var result = await _managerService.CreateEmployeeAsync(dto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
