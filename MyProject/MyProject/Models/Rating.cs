using System;
using System.Collections.Generic;

namespace MyProject.Models;

public partial class Rating
{
    public int RatingId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int RatingValue { get; set; }

    public string? Review { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
