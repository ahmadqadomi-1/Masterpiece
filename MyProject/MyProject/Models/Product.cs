using System;
using System.Collections.Generic;

namespace MyProject.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public string? ProductDescriptionList1 { get; set; }

    public string? ProductDescriptionList2 { get; set; }

    public string? ProductDescriptionList3 { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public int? CategoryId { get; set; }

    public string? ProductImage { get; set; }

    public decimal? ProductRate { get; set; }

    public string? ProductImage2 { get; set; }

    public string? ProductImage3 { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
