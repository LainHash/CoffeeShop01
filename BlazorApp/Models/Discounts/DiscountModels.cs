using System.Text.Json.Serialization;

namespace BlazorApp.Models.Discounts
{
    public class DiscountModel
    {
        public int DiscountId { get; set; }
        public string? DiscountCode { get; set; }
        public string Type { get; set; } = string.Empty;
        public double Value { get; set; }
        public DateTime? ExpiredDate { get; set; }

        public string DisplayLabel => string.IsNullOrEmpty(DiscountCode)
            ? $"{(Type == "Percentage" ? $"{Value}%" : $"{Value:N0}đ")} giảm"
            : $"{DiscountCode} - {(Type == "Percentage" ? $"{Value}%" : $"{Value:N0}đ")}";
    }

    public class DiscountListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public List<DiscountModel> List { get; set; } = new();
    }

    public class DiscountResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public DiscountModel? Data { get; set; }
    }
}
