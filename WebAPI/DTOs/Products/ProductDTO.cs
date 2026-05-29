namespace WebAPI.DTOs.Products
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public Guid PublicId { get; set; }

        public string ProductName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsMadeToOrder { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UnitsInstock { get; set; }
    }
}
