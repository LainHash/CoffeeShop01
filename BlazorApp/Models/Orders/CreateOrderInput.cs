using System;
using System.Collections.Generic;

namespace BlazorApp.Models.Orders
{
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
}
