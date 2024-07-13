using System;
using System.Collections.Generic;

namespace Photography.Models;

public partial class PhotoCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryPhoto { get; set; } = null!;

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
