using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tableService.GetAllAsync();
            return Ok(new
            {
                success = true,
                message = result.Message,
                data = result.Data
            });
        }

        [HttpGet("floor/{floorNumber}")]
        public async Task<IActionResult> GetAllByFloor(int floorNumber)
        {
            var result = await _tableService.GetAllByFloorAsync(floorNumber);
            return Ok(new
            {
                success = true,
                message = result.Message,
                data = result.Data
            });
        }

        [HttpGet("{floorNumber}/{tableNumber}")]
        public async Task<IActionResult> GetOne(int floorNumber, int tableNumber)
        {
            var result = await _tableService.GetOneAsync(floorNumber, tableNumber);
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
                data = result.Data
            });
        }

        public class UpdateStatusRequest
        {
            public string Status { get; set; } = string.Empty;
        }

        [HttpPut("{tableId}/status")]
        public async Task<IActionResult> UpdateStatus(int tableId, [FromBody] UpdateStatusRequest req)
        {
            var result = await _tableService.UpdateStatusAsync(tableId, req.Status);
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
                data = result.Data
            });
        }
    }
}
