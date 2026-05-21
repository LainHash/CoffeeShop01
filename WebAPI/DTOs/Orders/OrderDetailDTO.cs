using WebAPI.Models;

namespace WebAPI.DTOs.Orders
{
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
}
