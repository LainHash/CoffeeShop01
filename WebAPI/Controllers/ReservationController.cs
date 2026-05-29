using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Reservations;
using WebAPI.Helpers.Extensions;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationDTO dto,
            [FromServices] IValidator<CreateReservationDTO> validator)
        {
            var error = await validator.ValidateAndReturnError(dto);
            if (error != null) return error;

            var result = await _reservationService.CreateAsync(dto);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reservationService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _reservationService.GetByIdAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("Customer/{customerPublicId:guid}")]
        public async Task<IActionResult> GetByCustomer(Guid customerPublicId)
        {
            var result = await _reservationService.GetByCustomerAsync(customerPublicId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("Week")]
        public async Task<IActionResult> GetByWeek([FromQuery] DateTime weekStart)
        {
            var result = await _reservationService.GetByWeekAsync(weekStart);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReservationDTO dto,
            [FromServices] IValidator<UpdateReservationDTO> validator)
        {
            var error = await validator.ValidateAndReturnError(dto);
            if (error != null) return error;

            var result = await _reservationService.UpdateAsync(id, dto);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
