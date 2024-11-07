using System;
using System.Collections.Generic;

namespace masterpieceDashboard.Server.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string? NewsName { get; set; }

    public string? YoutubeUrl { get; set; }

    public string? NewsDescription { get; set; }

    public DateOnly? NewsDate { get; set; }
}
