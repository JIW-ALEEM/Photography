using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photography.Models;

public partial class PhotoCategory
{
    public int CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; } = null!;

    public string? CategoryPhoto { get; set; }
    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
