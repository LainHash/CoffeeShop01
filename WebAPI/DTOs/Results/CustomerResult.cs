using WebAPI.DTOs.Accounts.Customers;

namespace WebAPI.DTOs.Results
{
    public class CustomerResult : ResultBase
    {
        public CustomerDTO? Customer { get; set; }

        public CustomerResult(bool success, string message, CustomerDTO? customer = null) : base(success, message)
        {
            Customer = customer;
        }
    }
}
