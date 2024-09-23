using System;
using System.Collections.Generic;

namespace MyProject.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Message { get; set; }

    public DateTime? SentDate { get; set; }
}
