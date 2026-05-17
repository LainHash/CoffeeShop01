using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class TableEntity
{
    public int TableId { get; set; }

    public string Shape { get; set; } = null!;

    public int TableNumber { get; set; }

    public int FloorNumber { get; set; }

    public int RecommendedCapacity { get; set; }

    public string Status { get; set; } = null!;

    public bool? IsActive { get; set; }

    public int MaxCapacity { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
