using System;
using System.Collections.Generic;

namespace Photography.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public DateTime BookingDate { get; set; }

    public TimeSpan BookingTime { get; set; }

    public string BookingStatus { get; set; } = null!;

    public string BookingAddress { get; set; } = null!;

    public string BookingContact { get; set; } = null!;

    public int BookingPlanId { get; set; }

    public DateTime? BookingCreatedAt { get; set; }

    public DateTime? BookingUpdatedAt { get; set; }

    public int BookingUserId { get; set; }

    public virtual Plan BookingPlan { get; set; } = null!;

    public virtual User BookingUser { get; set; } = null!;
}
