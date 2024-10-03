using System;
using System.Collections.Generic;

namespace MyProject.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public string? ProjectName { get; set; }

    public string? ProjectType { get; set; }

    public DateTime? ProjectDate { get; set; }

    public string? ProjectImage { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }
}
