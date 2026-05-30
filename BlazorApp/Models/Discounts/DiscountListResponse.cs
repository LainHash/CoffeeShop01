using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApp.Models.Discounts
{
    public class DiscountListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<DiscountModel> List { get; set; } = new();
    }
}
