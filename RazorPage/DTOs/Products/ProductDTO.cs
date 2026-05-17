namespace RazorPage.DTOs.Products
{
    public class ProductDTO
    {
        public Guid PublicId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UnitsInstock { get; set; }
    }
}
