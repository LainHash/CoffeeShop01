using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Reservations;
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
        public async Task<IActionResult> Create([FromBody] CreateReservationDTO dto)
        {
            var result = await _reservationService.CreateAsync(dto);
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
                reservation = result.Reservation
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reservationService.GetAllAsync();
            return Ok(new
            {
                success = result.Success,
                message = result.Message,
                reservations = result.Reservations
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _reservationService.GetByIdAsync(id);
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
                reservation = result.Reservation
            });
        }

        [HttpGet("Customer/{customerPublicId:guid}")]
        public async Task<IActionResult> GetByCustomer(Guid customerPublicId)
        {
            var result = await _reservationService.GetByCustomerAsync(customerPublicId);
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
                reservations = result.Reservations
            });
        }

        [HttpGet("Week")]
        public async Task<IActionResult> GetByWeek([FromQuery] DateTime weekStart)
        {
            var result = await _reservationService.GetByWeekAsync(weekStart);
            return Ok(new
            {
                success = result.Success,
                message = result.Message,
                reservations = result.Reservations
            });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReservationDTO dto)
        {
            var result = await _reservationService.UpdateAsync(id, dto);
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
                reservation = result.Reservation
            });
        }

        [HttpDelete("{id:int}/Cancel")]
        public async Task<IActionResult> Cancel(int id, [FromQuery] Guid customerPublicId)
        {
            var result = await _reservationService.CancelAsync(id, customerPublicId);
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
