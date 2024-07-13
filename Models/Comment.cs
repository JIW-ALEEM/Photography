using System;
using System.Collections.Generic;

namespace Photography.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int BlogId { get; set; }

    public int UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
