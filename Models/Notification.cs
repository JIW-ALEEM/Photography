﻿using System;
using System.Collections.Generic;

namespace Photography.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public string NotificationMessage { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public bool? IsRead { get; set; }

    public virtual User User { get; set; } = null!;
}
