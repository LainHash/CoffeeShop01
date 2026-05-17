using System.Diagnostics.Eventing.Reader;
using WebAPI.DTOs.Orders;

namespace WebAPI.DTOs.Results
{
    public class OrderResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public OrderDTO? Order { get; set; }
        public List<OrderDTO>? Orders { get; set; }

        public OrderResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public OrderResult(bool success, string message, OrderDTO? order)
        {
            Success = success;
            Message = message;
            Order = order;
        }

        public OrderResult(bool success, string message, List<OrderDTO>? orders)
        {
            Success = success;
            Message = message;
            Orders = orders;
        }
    }
}
