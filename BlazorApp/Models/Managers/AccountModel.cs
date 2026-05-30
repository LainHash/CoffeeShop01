using System;

namespace BlazorApp.Models.Managers
{
    public class AccountModel
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
