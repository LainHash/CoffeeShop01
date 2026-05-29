using System;

namespace BlazorApp.Models.Products
{
    public class ProductModel
    {
        public Guid PublicId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UnitsInstock { get; set; }
        public bool IsMadeToOrder { get; set; }
    }
}
