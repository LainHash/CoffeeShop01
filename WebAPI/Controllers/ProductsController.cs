using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Products;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productsService.GetAllProductsAsync();
            return Ok(new
            {
                success = true,
                message = "Lấy danh sách sản phẩm thành công!",
                list = products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneProduct(Guid id)
        {
            var result = await _productsService.GetOneProductAsync(id);
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
                product = result.Product
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO dto)
        {
            var result = await _productsService.CreateProductAsync(dto);
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
                product = result.Product
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDTO dto)
        {
            var result = await _productsService.UpdateProductAsync(id, dto);
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
                product = result.Product
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productsService.DeleteProductAsync(id);
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
                message = result.Message
            });
        }
    }
}
