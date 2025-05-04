using System;
using System.Collections.Generic;

namespace ProfileServices.Models;

public partial class Merchant
{
    public long MerchantId { get; set; }

    public long UserId { get; set; }

    public string? MerchantName { get; set; } = null!;

    public string? MerchantAddress { get; set; } = null!;

    public string? ContactNumber { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User User { get; set; } = null!;
}
