using System;

namespace BlazorApp.Models.Auth
{
    public class AuthCustomerData
    {
        public Guid PublicId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
