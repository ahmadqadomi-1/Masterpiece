using System;
using System.Collections.Generic;

namespace AlQadomy.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public int? CategoryId { get; set; }

    public decimal? Discount { get; set; }

    public string? ProductImage { get; set; }

    public decimal? Rate { get; set; }

    public virtual Category? Category { get; set; }
}
