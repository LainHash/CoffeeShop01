using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Orders.Create;
using WebAPI.DTOs.Orders.Update;
using WebAPI.Helpers.Extensions;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var result = await _orderService.GetOneAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDTO request,
            [FromServices] IValidator<CreateOrderDTO> validator)
        {
            var error = await validator.ValidateAndReturnError(request);
            if (error != null) return error;

            var result = await _orderService.CreateAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("{id}/Checkout")]
        public async Task<IActionResult> Checkout(Guid id, [FromQuery] bool confirm, [FromQuery] string paymentMethod = "Cash", [FromQuery] string? note = null)
        {
            var result = await _orderService.Checkout(id, confirm, paymentMethod, note);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
