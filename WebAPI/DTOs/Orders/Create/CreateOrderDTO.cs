namespace WebAPI.DTOs.Orders.Create
{
    public class CreateOrderDTO
    {
        public int TableId { get; set; }

        public Guid EmployeePublicId { get; set; }

        public int? ReservationId { get; set; }

        public string Status { get; set; } = null!;

        public decimal SubTotal { get; set; }

        public int? DiscountId { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Note { get; set; }

        public List<CreateOrderDetailDTO> OrderDetails { get; set; } = new List<CreateOrderDetailDTO>(); 
    }
}
