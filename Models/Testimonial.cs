using System;
using System.Collections.Generic;

namespace Photography.Models;

public partial class Testimonial
{
    public int TestimonialId { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public int? StarRating { get; set; }

    public virtual User User { get; set; } = null!;
}
