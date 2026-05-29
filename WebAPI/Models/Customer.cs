using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public Guid PublicId { get; set; }

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
