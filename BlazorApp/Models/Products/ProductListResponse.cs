using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApp.Models.Products
{
    public class ProductListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<ProductModel> List { get; set; } = new();
    }
}
