using System;
using System.Collections.Generic;

namespace Photography.Models;

public partial class Photo
{
    public int PhotoId { get; set; }

    public int CategoryId { get; set; }

    public string PhotoUrl { get; set; } = null!;

    public string PhotoTitle { get; set; } = null!;

    public string PhotoDesc { get; set; } = null!;

    public virtual PhotoCategory Category { get; set; } = null!;
}
