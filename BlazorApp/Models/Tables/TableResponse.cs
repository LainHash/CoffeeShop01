namespace BlazorApp.Models.Tables
{
    public class TableResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public TableModel? Data { get; set; }
    }
}
