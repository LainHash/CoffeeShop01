using System;
using System.Collections.Generic;

namespace BlazorApp.Models.Orders
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public Guid PublicId { get; set; }
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
        public DateTime CreatedAt { get; set; }
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
}
