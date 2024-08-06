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

    public string? UserImg { get; set; }

    public string? UserPet { get; set; }

    public int? UserRoleId { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Like> Likes { get; } = new List<Like>();

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();

    public virtual ICollection<Testimonial> Testimonials { get; } = new List<Testimonial>();

    public virtual Role? UserRole { get; set; }
}
