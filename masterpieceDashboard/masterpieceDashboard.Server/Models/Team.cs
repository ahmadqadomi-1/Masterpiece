using System;
using System.Collections.Generic;

namespace masterpieceDashboard.Server.Models;

public partial class Team
{
    public int TeamId { get; set; }

    public string? TeamName { get; set; }

    public string? TeamImg { get; set; }

    public string? Profession { get; set; }

    public string? TeamPhoneNum { get; set; }
}
