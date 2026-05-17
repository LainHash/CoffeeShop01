namespace WebAPI.DTOs.Accounts.Customers
{
    public class AccountDTO
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
