using System;

namespace BlazorApp.Models.Managers
{
    public class EmployeeModel
    {
        public Guid PublicId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Position { get; set; }
        public AccountModel User { get; set; } = new();
    }
}
