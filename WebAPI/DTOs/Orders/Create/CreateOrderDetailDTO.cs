namespace WebAPI.DTOs.Orders.Create
{
    public class CreateOrderDetailDTO
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal LineTotal { get; set; }
    }
}
