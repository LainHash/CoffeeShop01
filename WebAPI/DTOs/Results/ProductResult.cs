using WebAPI.DTOs.Products;

namespace WebAPI.DTOs.Results
{
    public class ProductResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public ProductDTO? Product { get; set; }

        public ProductResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
        public ProductResult(bool success, string message, ProductDTO? product)
        {
            this.Success = success;
            this.Message = message;
            this.Product = product;
        }
    }
}
