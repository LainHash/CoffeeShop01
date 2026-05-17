using WebAPI.DTOs.Accounts.Customers;

namespace WebAPI.DTOs.Results
{
    public class CustomerResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public CustomerDTO? Customer { get; set; }

        public CustomerResult(bool success, string message, CustomerDTO? customer = null)
        {
            Success = success;
            Message = message;
            Customer = customer;
        }
    }
}
