using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photography.Models;

public partial class PhotoCategory
{
    public int CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; } = null!;
    [Required]
    public string CategoryPhoto { get; set; } = null!;

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
