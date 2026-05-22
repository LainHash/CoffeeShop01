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

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetInfo(Guid id)
        //{
        //    var result = await _managerService.GetInfoAsync(id);
        //    if (!result.Success)
        //    {
        //        return BadRequest(new
        //        {
        //            success = false,
        //            message = result.Message
        //        });
        //    }
        //    return Ok(new
        //    {
        //        success = true,
        //        message = result.Message,
        //        manager = result.Manager
        //    });
        //}

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
                manager = result.Manager
            });
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(Guid id, UpdateManagerInfoDTO dto)
        //{
        //    var result = await _managerService.UpdateAsync(id, dto);
        //    if (!result.Success)
        //    {
        //        return BadRequest(new
        //        {
        //            success = false,
        //            message = result.Message
        //        });
        //    }
        //    return Ok(new
        //    {
        //        success = true,
        //        message = result.Message,
        //        manager = result.Manager
        //    });
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var result = await _managerService.DeleteAsync(id);
        //    if (!result.Success)
        //    {
        //        return BadRequest(new
        //        {
        //            success = false,
        //            message = result.Message
        //        });
        //    }
        //    return Ok(new
        //    {
        //        success = true,
        //        message = result.Message
        //    });
        //}

        //[HttpPut("{id}/change-password")]
        //public async Task<IActionResult> ChangePassword(Guid id, PasswordChangeDTO dto)
        //{
        //    var result = await _managerService.ChangePasswordAsync(id, dto);
        //    if (!result.Success)
        //    {
        //        return BadRequest(new
        //        {
        //            success = false,
        //            message = result.Message
        //        });
        //    }
        //    return Ok(new
        //    {
        //        success = true,
        //        message = result.Message,
        //        manager = result.Manager
        //    });
        //}
    }
}
