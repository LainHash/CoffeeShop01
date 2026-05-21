using WebAPI.DTOs.Accounts.Customers;

namespace WebAPI.DTOs.Results
{
    public class CustomerResult : ResultBase
    {
        public CustomerDTO? Customer { get; set; }
        public List<CustomerDTO>? Customers { get; set; }

        public CustomerResult(bool success, string message,
            CustomerDTO? customer = null,
            List<CustomerDTO>? customers = null) : base(success, message)
        {
            Customer = customer;
            Customers = customers;
        }
    }
}
