namespace WebAPI.DTOs.Accounts.Customers
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public Guid? PublicId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
    }
}
