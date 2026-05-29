using System;
using BlazorApp.Helpers.Constants;

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
            ? $"{(Type == DiscountConstants.Percent ? $"{Value}%" : $"{Value:N0}đ")} giảm"
            : $"{DiscountCode} - {(Type == DiscountConstants.Percent ? $"{Value}%" : $"{Value:N0}đ")}";
    }
}
