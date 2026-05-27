using System.Text.Json.Serialization;

namespace BlazorApp.Models.Orders
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public Guid? PublicId { get; set; }
        public int TableId { get; set; }
        public int EmployeeId { get; set; }
        public int? ReservationId { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public int? DiscountId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Note { get; set; }
        public int TableNumber { get; set; }
        public int FloorNumber { get; set; }
        public string? EmployeeName { get; set; }
        public string? DiscountCode { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; } = new();

        public string StatusLabel => Status switch
        {
            "Unpaid"    => "Chưa thanh toán",
            "Paid"      => "Đã thanh toán",
            "Cancelled" => "Đã huỷ",
            _           => Status
        };
        public string StatusBadgeClass => Status switch
        {
            "Unpaid"    => "badge-unpaid",
            "Paid"      => "badge-paid",
            "Cancelled" => "badge-cancelled",
            _           => "badge-secondary"
        };
    }

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

    public class CreateOrderInput
    {
        public int TableId { get; set; }
        public Guid EmployeePublicId { get; set; }
        public int? ReservationId { get; set; }
        public string Status { get; set; } = "Unpaid";
        public decimal SubTotal { get; set; }
        public int? DiscountId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Note { get; set; }
        public List<CreateOrderDetailInput> OrderDetails { get; set; } = new();
    }

    public class CreateOrderDetailInput
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    // Cart item for POS UI
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal => UnitPrice * Quantity;
    }

    // API Response wrappers
    public class OrderListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<OrderModel>? Orders { get; set; }
    }

    public class OrderSingleResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public OrderModel? Order { get; set; }
        public OrderModel? Data { get; set; }
        public OrderModel? GetOrder() => Order ?? Data;
    }
}
