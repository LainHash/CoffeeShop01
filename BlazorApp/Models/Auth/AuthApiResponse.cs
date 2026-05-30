namespace BlazorApp.Models.Auth
{
    public class AuthApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public AuthCustomerData? Customer { get; set; }
        public AuthManagerData? Data { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
