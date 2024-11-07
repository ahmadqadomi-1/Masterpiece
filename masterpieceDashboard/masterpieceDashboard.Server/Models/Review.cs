using System;
using System.Collections.Generic;

namespace masterpieceDashboard.Server.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? ProductId { get; set; }

    public int? UserId { get; set; }

    public string? CommentText { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public int RatingValue { get; set; }

    public DateTime? ReviewTime { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
