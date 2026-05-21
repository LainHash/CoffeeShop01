using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _discountService.GetAllAsync();
            return Ok(new
            {
                success = true,
                message = result.Message,
                list = result.Discounts
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await _discountService.GetOneAsync(id);
            if(!result.Success)
            {
                return NotFound(new
                {
                    success = false,
                    message = result.Message
                });
            }
            return Ok(new
            {
                success = true,
                message = result.Message,
                data = result.Discount
            });
        }
    }
}
