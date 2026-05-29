namespace BlazorApp.Models.Products
{
    public class CreateProductInput
    {
        public string ProductName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? UnitsInstock { get; set; }
        public bool IsMadeToOrder { get; set; }
    }
}
