using WebAPI.DTOs.Discounts;

namespace WebAPI.DTOs.Results
{
    public class DiscountResult : ResultBase
    {
        public DiscountDTO? Discount { get; set; }
        public List<DiscountDTO>? Discounts { get; set; }

        public DiscountResult(bool success, string message) : base(success, message)
        {
        }

        public DiscountResult(bool success, string message, DiscountDTO discount) : base(success, message)
        {
            Discount = discount;
        }

        public DiscountResult(bool success, string message, List<DiscountDTO> discounts) : base(success, message)
        {
            Discounts = discounts;
        }
    }
}
