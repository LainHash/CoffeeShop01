namespace BlazorApp.Models.Orders
{
    public class OrderSingleResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public OrderModel? Order { get; set; }
        public OrderModel? Data { get; set; }
        public OrderModel? GetOrder() => Order ?? Data;
    }
}
