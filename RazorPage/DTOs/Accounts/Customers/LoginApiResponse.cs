namespace RazorPage.DTOs.Accounts.Customers
{
    public class LoginApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public CustomerDTO? Customer { get; set; }
    }
}
