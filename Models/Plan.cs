using System;
using System.Collections.Generic;

namespace Photography.Models;

public partial class Plan
{
    public int PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public string? List1 { get; set; }

    public string? List2 { get; set; }

    public string? List3 { get; set; }

    public string? List4 { get; set; }

    public string? List5 { get; set; }

    public string? List6 { get; set; }

    public long Price { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
