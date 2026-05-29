using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int UserId { get; set; }

    public Guid PublicId { get; set; }

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Position { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
