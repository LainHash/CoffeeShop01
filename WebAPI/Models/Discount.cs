using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string? DiscountCode { get; set; }

    public string Type { get; set; } = null!;

    public double Value { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
