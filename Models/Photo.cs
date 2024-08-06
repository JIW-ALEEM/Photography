using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photography.Models;

public partial class Photo
{
    public int PhotoId { get; set; }

    public int CategoryId { get; set; }

    public string? PhotoUrl { get; set; }
    [Required]
    public string PhotoTitle { get; set; } = null!;
    [Required]
    public string PhotoDesc { get; set; } = null!;

    public virtual PhotoCategory Category { get; set; } = null!;
}
