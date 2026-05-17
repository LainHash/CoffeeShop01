using RazorPage.Pages;

namespace RazorPage.DTOs.Products
{
    public class ProductListResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<ProductDTO>? List { get; set; }
    }
}
