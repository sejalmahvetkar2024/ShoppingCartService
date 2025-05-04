using System;
using System.Collections.Generic;

namespace ProfileServices.Models;

public partial class Cart
{
    public long CartId { get; set; }

    public long UserId { get; set; }

    public decimal? TotalPrice { get; set; }

    public long? ProductId { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual Product? Product { get; set; }

    public virtual User User { get; set; } = null!;
}
