using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CartService.Models;

public partial class Item
{
    public long ItemId { get; set; }

    public long CartId { get; set; }

    public long ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual Cart Cart { get; set; } = null!;
}
