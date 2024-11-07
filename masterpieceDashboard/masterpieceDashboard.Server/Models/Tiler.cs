using System;
using System.Collections.Generic;

namespace masterpieceDashboard.Server.Models;

public partial class Tiler
{
    public int TilerId { get; set; }

    public string? TilerName { get; set; }

    public string? TilerImg { get; set; }

    public string? Profession { get; set; }

    public string? TilerPhoneNum { get; set; }
}
