using System;
using System.Collections.Generic;

namespace ProfileServices.Models;

public partial class Item
{
    public long ItemId { get; set; }

    public long CartId { get; set; }

    public long ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
