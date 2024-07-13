using System;
using System.Collections.Generic;

namespace Photography.Models;

public partial class Like
{
    public int LikeId { get; set; }

    public int BlogId { get; set; }

    public int UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
