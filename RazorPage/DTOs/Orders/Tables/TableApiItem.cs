namespace RazorPage.DTOs.Orders.Tables
{
    public class TableApiItem
    {
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public int FloorNumber { get; set; }
        public int RecommendedCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
