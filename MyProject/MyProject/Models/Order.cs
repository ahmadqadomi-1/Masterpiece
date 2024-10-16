using System;
using System.Collections.Generic;

namespace MyProject.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? PaymentMethod { get; set; }

    public string? OrderStatus { get; set; }

    public DateOnly? OrderDate { get; set; }

    public string? Comment { get; set; }

    public virtual User? User { get; set; }
}
