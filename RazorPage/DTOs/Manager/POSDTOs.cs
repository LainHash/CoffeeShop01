using System.Collections.Generic;

namespace RazorPage.DTOs.Manager
{
    public class CreateOrderInput
    {
        public int TableId { get; set; }
        public int EmployeeId { get; set; }
        public string Status { get; set; } = "Pending";
        public int? DiscountId { get; set; }
        public List<CreateOrderDetailInput> OrderDetails { get; set; } = new();
    }

    public class CreateOrderDetailInput
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class TableResponse
    {
        public List<TableEntityDTO> List { get; set; } = new();
    }

    public class TableEntityDTO
    {
        public int TableId { get; set; }
        public string Shape { get; set; } = "";
        public int TableNumber { get; set; }
        public int FloorNumber { get; set; }
    }

    public class ProductResponse
    {
        public List<POSProductDTO> List { get; set; } = new();
    }

    public class POSProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = "";
    }

    public class DiscountResponse
    {
        public List<POSDiscountDTO> List { get; set; } = new();
    }

    public class POSDiscountDTO
    {
        public int DiscountId { get; set; }
        public string DiscountCode { get; set; } = "";
        public string Type { get; set; } = "";
        public double Value { get; set; }
    }
}
