namespace BlazorApp.Models.Tables
{
    public class TableModel
    {
        public int TableId { get; set; }
        public string Shape { get; set; } = string.Empty;
        public int TableNumber { get; set; }
        public int FloorNumber { get; set; }
        public int RecommendedCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public string Status { get; set; } = string.Empty;

        public string DisplayName => $"Bàn {TableNumber} - Tầng {FloorNumber}";
        public string StatusBadgeClass => Status switch
        {
            "Available" => "badge-available",
            "Occupied"  => "badge-occupied",
            "Reserved"  => "badge-reserved",
            _           => "badge-maintenance"
        };
        public string StatusLabel => Status switch
        {
            "Available"   => "Trống",
            "Occupied"    => "Đang dùng",
            "Reserved"    => "Đặt trước",
            "Maintenance" => "Bảo trì",
            _             => Status
        };
    }
}
