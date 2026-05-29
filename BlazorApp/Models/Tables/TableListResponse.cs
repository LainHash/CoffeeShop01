using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApp.Models.Tables
{
    public class TableListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<TableModel> List { get; set; } = new();
    }
}
