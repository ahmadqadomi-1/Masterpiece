using System;
using System.Collections.Generic;

namespace masterpieceDashboard.Server.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? PaymentMethod { get; set; }

    public string? OrderStatus { get; set; }

    public DateOnly? OrderDate { get; set; }

    public string? Comment { get; set; }

    public int? OrderStatusId { get; set; }

    public virtual OrderStatus? OrderStatusNavigation { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
