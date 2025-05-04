using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CartService.Models;

public partial class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long CartId { get; set; }

    [ForeignKey("UserId")]
    public long UserId { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? TotalPrice { get; set; }

    public long? ProductId { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual User? User { get; set; } = null!;
}
