using System;
using System.Collections.Generic;

namespace OrderService.Models;

public partial class Wallet
{
    public long WalletId { get; set; }

    public long UserId { get; set; }

    public decimal? Balance { get; set; }

    public long? OrderId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
