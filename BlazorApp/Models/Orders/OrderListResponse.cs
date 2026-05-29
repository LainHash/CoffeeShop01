using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApp.Models.Orders
{
    // API Response wrappers
    public class OrderListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<OrderModel>? Orders { get; set; }
    }
}
