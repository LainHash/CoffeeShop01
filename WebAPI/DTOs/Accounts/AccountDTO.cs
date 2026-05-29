namespace WebAPI.DTOs.Accounts
{
    public class AccountDTO
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int RoleId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
