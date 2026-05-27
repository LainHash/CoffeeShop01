using System;

namespace RazorPage.DTOs.Auth
{
    public class UserProfile
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string? Position { get; set; }
    }

    public class CustomerInfoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public CustomerProfileDTO? Data { get; set; }
    }

    public class ManagerInfoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public ManagerProfileDTO? Data { get; set; }
    }

    public class CustomerProfileDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public AccountProfileDTO User { get; set; } = new();
    }

    public class ManagerProfileDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Position { get; set; }
        public AccountProfileDTO User { get; set; } = new();
    }

    public class AccountProfileDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
