using RazorPage.DTOs.Accounts;

namespace RazorPage.DTOs.Customers
{
    public class CustomerDTO
    {
        public Guid PublicId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public AccountDTO User { get; set; } = new AccountDTO();
    }
}
