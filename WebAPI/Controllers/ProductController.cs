using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Products.Create;
using WebAPI.DTOs.Products.Update;
using WebAPI.Helpers.Extensions;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productsService;

        public ProductController(IProductService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productsService.GetAllAsync();
            return Ok(new
            {
                success = true,
                message = result.Message,
                list = result.Products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var result = await _productsService.GetOneAsync(id);
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
        public async Task<IActionResult> Create(CreateProductDTO dto, [FromServices] IValidator<CreateProductDTO> validator)
        {
            var error = await validator.ValidateAndReturnError(dto);
            if (error != null)
            {
                return error;
            }

            var result = await _productsService.CreateAsync(dto);
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
        public async Task<IActionResult> Update(Guid id, UpdateProductDTO dto, [FromServices] IValidator<UpdateProductDTO> validator)
        {
            var error = await validator.ValidateAndReturnError(dto);
            if (error != null)
            {
                return error;
            }

            var result = await _productsService.UpdateAsync(id, dto);
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
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productsService.DeleteAsync(id);
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
