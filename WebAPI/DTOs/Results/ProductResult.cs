using WebAPI.DTOs.Products;

namespace WebAPI.DTOs.Results
{
    public class ProductResult : ResultBase
    {
        public ProductDTO? Product { get; set; }
        public List<ProductDTO>? Products { get; set; }

        public ProductResult(bool success, string message) : base(success, message)
        {

        }

        public ProductResult(bool success, string message, List<ProductDTO>? products) : base(success, message) 
        {
            Products = products;
        }

        public ProductResult(bool success, string message, ProductDTO? product = null) : base(success, message) 
        {
            Product = product;
        }
    }
}
