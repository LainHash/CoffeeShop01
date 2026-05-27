namespace BlazorApp.Models.Auth
{
    public class LoginInput
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public AuthCustomerData? Customer { get; set; }
        public AuthManagerData? Manager { get; set; }
    }

    public class AuthCustomerData
    {
        public Guid PublicId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }

    public class AuthManagerData
    {
        public Guid PublicId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }

    public class RegisterEmployeeInput
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
    }
}
