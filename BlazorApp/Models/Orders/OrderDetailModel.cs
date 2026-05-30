namespace BlazorApp.Models.Orders
{
    public class OrderDetailModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public string? ProductName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
