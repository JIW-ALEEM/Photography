using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photography.Models;

public partial class User
{
    public int UserId { get; set; }
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string UserEmail { get; set; } = null!;
    [Required]
    public string UserPassword { get; set; } = null!;

    public long? UserPhone { get; set; }

    public string UserImg { get; set; } = null!;

    public string? UserPet { get; set; }

    public int? UserRoleId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();

    public virtual Role? UserRole { get; set; }
}
