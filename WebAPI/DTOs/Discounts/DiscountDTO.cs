namespace WebAPI.DTOs.Discounts
{
    public class DiscountDTO
    {
        public int DiscountId { get; set; }

        public string? DiscountCode { get; set; }

        public string Type { get; set; } = null!;

        public double Value { get; set; }

        public DateTime? ExpiredDate { get; set; }
    }
}
