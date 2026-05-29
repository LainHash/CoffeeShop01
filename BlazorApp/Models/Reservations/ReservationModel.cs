using System;
using System.Text.Json.Serialization;

namespace BlazorApp.Models.Reservations
{
    public class ReservationModel
    {
        [JsonPropertyName("publicId")]
        public Guid ReservationId { get; set; }
        public string CustomerFullName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string? TableName { get; set; }
        public int TableNumber { get; set; }
        public int FloorNumber { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }

        public string StatusLabel => Status switch
        {
            "Pending"   => "Chờ xác nhận",
            "Confirmed" => "Đã xác nhận",
            "Cancelled" => "Đã huỷ",
            "Completed" => "Hoàn thành",
            _           => Status
        };
        public string StatusBadgeClass => Status switch
        {
            "Pending"   => "badge-warning",
            "Confirmed" => "badge-success",
            "Cancelled" => "badge-danger",
            "Completed" => "badge-info",
            _           => "badge-secondary"
        };
    }
}
