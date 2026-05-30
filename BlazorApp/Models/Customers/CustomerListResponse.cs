using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApp.Models.Customers
{
    public class CustomerListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<CustomerModel> List { get; set; } = new();
    }
}
