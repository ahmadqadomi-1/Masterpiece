﻿using System;
using System.Collections.Generic;

namespace MyProject.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? ProductId { get; set; }

    public int? UserId { get; set; }

    public string? CommentText { get; set; }

    public int? Rating { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? UserName { get; set; }

    public int RatingValue { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
