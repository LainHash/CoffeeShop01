namespace WebAPI.DTOs.Accounts.Managers
{
    public class RegisterDTO
    {

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;

        public string? Phone { get; set; }

        public string Position { get; set; } = null!;
    }
}
