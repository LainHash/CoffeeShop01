namespace WebAPI.DTOs.Orders
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public Guid? PublicId { get; set; }

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
        public DateTime CreatedAt { get; set; }

        public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
    }
}
