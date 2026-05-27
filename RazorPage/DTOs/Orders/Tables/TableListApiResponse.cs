namespace RazorPage.DTOs.Orders.Tables
{
    public class TableListApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<TableApiItem> Data { get; set; } = new();
    }
}
