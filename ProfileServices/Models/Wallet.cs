using System;
using System.Collections.Generic;

namespace ProfileServices.Models;

public partial class Wallet
{
    public long WalletId { get; set; }

    public long UserId { get; set; }

    public decimal? Balance { get; set; }

    public long? OrderId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Statement> Statements { get; set; } = new List<Statement>();

    public virtual User User { get; set; } = null!;
}
