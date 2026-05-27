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
            return Ok(result);
        }

        [HttpGet("floor/{floorNumber}")]
        public async Task<IActionResult> GetAllByFloor(int floorNumber)
        {
            var result = await _tableService.GetAllByFloorAsync(floorNumber);
            return Ok(result);
        }

        [HttpGet("floor/{floorNumber}/{tableNumber}")]
        public async Task<IActionResult> GetOne(int floorNumber, int tableNumber)
        {
            var result = await _tableService.GetOneAsync(floorNumber, tableNumber);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        public class UpdateStatusRequest
        {
            public string Status { get; set; } = string.Empty;
        }

        [HttpPut("floor/{floorNumber}/{tableNumber}/status")]
        public async Task<IActionResult> UpdateStatus(int floorNumber, int tableNumber, [FromBody] UpdateStatusRequest req)
        {
            var result = await _tableService.UpdateStatusAsync(floorNumber, tableNumber, req.Status);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
