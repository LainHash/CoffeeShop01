using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _customerService.LoginAsync(dto);
            if (!result.Success)
            {
                return Ok(new
                {
                    success = false,
                    message = result.Message
                });
            }
            return Ok(new
            {
                success = true,
                message = result.Message,
                customer = result.Customer  
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfo(Guid id)
        {
            var result = await _customerService.GetInfoAsync(id);
            if (!result.Success)
            {
                return Ok(new
                {
                    success = false,
                    message = result.Message
                });
            }
            return Ok(new
            {
                success = true,
                message = result.Message,
                customer = result.Customer
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var result = await _customerService.RegisterAsync(dto);
            if (!result.Success)
            {
                return Ok(new
                {
                    success = false,
                    message = result.Message
                });
            }
            return Ok(new
            {
                success = true,
                message = result.Message
            });
        }
    }
}
