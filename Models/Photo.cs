using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photography.Models;

public partial class Photo
{
    public int PhotoId { get; set; }
    [Required]
    public int PhotoCategoryId { get; set; }

    public string? PhotoUrl { get; set; }

    [Required]
    public string PhotoTitle { get; set; } = null!;
    [Required]
    public string PhotoDesc { get; set; } = null!;

    public virtual PhotoCategory PhotoCategory { get; set; } = null!;
}
