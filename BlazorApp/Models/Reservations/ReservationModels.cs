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

    public class CreateReservationInput
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime ReservationTime { get; set; } = DateTime.Now.AddDays(1);
        public int NumberOfGuests { get; set; } = 2;
        public string? Note { get; set; }
        public int? TableId { get; set; }
    }

    public class UpdateReservationInput
    {
        public string Status { get; set; } = string.Empty;
        public int? TableId { get; set; }
        public string? Note { get; set; }
    }

    public class ReservationListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<ReservationModel> List { get; set; } = new();
    }

    public class ReservationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public ReservationModel? Data { get; set; }
    }
}
