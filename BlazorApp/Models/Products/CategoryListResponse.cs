using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApp.Models.Products
{
    public class CategoryListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<CategoryModel> List { get; set; } = new();
    }
}
