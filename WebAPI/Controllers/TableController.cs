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
                list = result.TableEntities
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
                list = result.TableEntities
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
                table = result.TableEntity
            });
        }
    }
}
