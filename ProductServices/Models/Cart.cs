using System;
using System.Collections.Generic;

namespace ProductServices.Models;

public partial class Cart
{
    public long CartId { get; set; }

    public long UserId { get; set; }

    public decimal? TotalPrice { get; set; }

    public long? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
