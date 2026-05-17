namespace WebAPI.DTOs.Accounts.Managers
{
    public class ManagerDTO
    {
        public Guid PublicId { get; set; }

        public string FullName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? Position { get; set; }

        public AccountDTO User { get; set; } = new AccountDTO();
    }
}
