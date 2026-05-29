using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorApp.Models.Reservations
{
    public class ReservationListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<ReservationModel> List { get; set; } = new();
    }
}
