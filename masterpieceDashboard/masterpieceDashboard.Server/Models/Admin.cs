﻿using System;
using System.Collections.Generic;

namespace masterpieceDashboard.Server.Models;

public partial class Admin
{
    public long Id { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }
}
