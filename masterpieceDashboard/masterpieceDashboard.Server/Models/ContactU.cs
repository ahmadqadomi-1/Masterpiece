using System;
using System.Collections.Generic;

namespace masterpieceDashboard.Server.Models;

public partial class ContactU
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Message { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }
}
