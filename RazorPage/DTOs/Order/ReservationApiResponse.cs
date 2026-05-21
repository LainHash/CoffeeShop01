using System.ComponentModel.DataAnnotations;

namespace RazorPage.DTOs.Order
{
    public class ReservationApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>Item phản hồi từ API cho mỗi đặt bàn</summary>
    public class ReservationApiItem
    {
        public int ReservationId { get; set; }
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
    }

    /// <summary>Phản hồi danh sách đặt bàn từ API</summary>
    public class ReservationListApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ReservationApiItem> Reservations { get; set; } = new();
    }

    public class ReservationInputModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string FullName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; } = string.Empty;

        public string? Date { get; set; }
        public string? Time { get; set; }

        public int NumberOfGuests { get; set; } = 1;
        public string? Note { get; set; }
        public int? TableId { get; set; }
    }

    public class TableApiItem
    {
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public int FloorNumber { get; set; }
        public int RecommendedCapacity { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class TableListApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<TableApiItem> List { get; set; } = new();
    }
}
