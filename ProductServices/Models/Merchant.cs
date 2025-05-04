using System;
using System.Collections.Generic;

namespace ProductServices.Models;

public partial class Merchant
{
    public long MerchantId { get; set; }

    public long UserId { get; set; }

    public string? MerchantName { get; set; } = null!;

    public string? MerchantAddress { get; set; } = null!;

    public string? ContactNumber { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
