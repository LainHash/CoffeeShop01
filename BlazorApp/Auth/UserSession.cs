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

        public bool IsCustomer => RoleId == 1;
        public bool IsEmployee => RoleId != 1;
        public bool IsManager => RoleId == 2;
    }
}
