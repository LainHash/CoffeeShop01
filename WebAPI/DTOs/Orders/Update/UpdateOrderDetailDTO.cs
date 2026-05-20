namespace WebAPI.DTOs.Orders.Update
{
    public class UpdateOrderDetailDTO
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal LineTotal { get; set; }
    }
}
