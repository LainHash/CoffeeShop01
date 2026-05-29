namespace BlazorApp.Models.Discounts
{
    public class DiscountResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public DiscountModel? Data { get; set; }
    }
}
