using System.Diagnostics.Eventing.Reader;
using WebAPI.DTOs.Orders;

namespace WebAPI.DTOs.Results
{
    public class OrderResult : ResultBase
    {

        public OrderDTO? Order { get; set; }
        public List<OrderDTO>? Orders { get; set; }

        public OrderResult(bool success, string message) : base(success, message) { }


        public OrderResult(bool success, string message, OrderDTO? order) : base(success, message)
        {
            Order = order;
        }

        public OrderResult(bool success, string message, List<OrderDTO>? orders) : base(success, message)
        {
            Orders = orders;
        }
    }
}
