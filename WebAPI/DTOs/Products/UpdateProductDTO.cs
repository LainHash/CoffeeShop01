namespace WebAPI.DTOs.Products
{
    public class UpdateProductDTO
    {
        public string ProductName { get; set; } = null!;

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string? Description { get; set; }

        public int UnitsInstock { get; set; }
    }
}
