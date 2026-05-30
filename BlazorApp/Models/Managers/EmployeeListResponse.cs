using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApp.Models.Managers
{
    public class EmployeeListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<EmployeeModel> List { get; set; } = new();
    }
}
