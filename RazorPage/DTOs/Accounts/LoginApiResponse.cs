using RazorPage.DTOs.Customers;

namespace RazorPage.DTOs.Accounts
{
    public class LoginApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public CustomerDTO? Data { get; set; }
    }
}
