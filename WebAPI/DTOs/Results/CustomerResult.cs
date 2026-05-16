using WebAPI.DTOs.Accounts.Customers;

namespace WebAPI.DTOs.Results
{
    public class CustomerResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public CustomerResult(bool success, string message) {
            Success = success;
            Message = message;
        }
    }
}
