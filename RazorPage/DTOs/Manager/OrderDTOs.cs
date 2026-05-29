using System;
using System.Collections.Generic;

namespace RazorPage.DTOs.Manager
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public Guid PublicId { get; set; }
        public int TableId { get; set; }
        public int EmployeeId { get; set; }
        public int? ReservationId { get; set; }
        public string Status { get; set; } = null!;
        public decimal SubTotal { get; set; }
        public int? DiscountId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Note { get; set; }
        public int TableNumber { get; set; }
        public int FloorNumber { get; set; }
        public string? EmployeeName { get; set; }
        public string? DiscountCode { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; } = new();
    }

    public class OrderDetailDTO
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

    public class OrderResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public OrderDTO? Order { get; set; }
        public List<OrderDTO>? Orders { get; set; }
    }
}
