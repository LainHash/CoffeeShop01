using System.Text.Json.Serialization;

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

    public class UpdateProductInput
    {
        public string ProductName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? UnitsInstock { get; set; }
        public bool IsMadeToOrder { get; set; }
    }

    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class ProductListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<ProductModel> List { get; set; } = new();
    }

    public class ProductResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public ProductModel? Data { get; set; }
    }

    public class CategoryListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<CategoryModel> List { get; set; } = new();
    }
}
