using System;
using System.Collections.Generic;

namespace ProductServices.Models;

public partial class Product
{
    public long ProductId { get; set; }

    public long MerchantId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Category { get; set; } = null!;

    public decimal Price { get; set; }

    public double? Rating { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public string Producttype { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Merchant? Merchant { get; set; } = null!;
}
