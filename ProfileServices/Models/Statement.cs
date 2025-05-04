using System;
using System.Collections.Generic;

namespace ProfileServices.Models;

public partial class Statement
{
    public long StatementId { get; set; }

    public long WalletId { get; set; }

    public decimal Amount { get; set; }

    public string TransactionType { get; set; } = null!;

    public DateTime? TransactionDate { get; set; }

    public long? OrderId { get; set; }

    public string? TransactionRemark { get; set; }

    public long? UserId { get; set; }

    public virtual User? User { get; set; }

    public virtual Wallet Wallet { get; set; } = null!;
}
