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

        public decimal DiscountAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Note { get; set; }

        public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
    }
}
