namespace BlazorApp.Auth
{
    public class UserSession
    {
        public Guid PublicId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int RoleId { get; set; }

        // RoleId = 1 → Customer, RoleId != 1 → Employee/Manager
        public bool IsCustomer => RoleId == 1;
        public bool IsEmployee => RoleId != 1;
        // RoleId = 2 → Manager (full access), 3 → Employee
        public bool IsManager => RoleId == 2;
    }
}
