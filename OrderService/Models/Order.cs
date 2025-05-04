using System;
using System.Collections.Generic;

namespace OrderService.Models;

public partial class Order
{
    public long OrderId { get; set; }

    public long? UserId { get; set; }

    public long? AddressId { get; set; }

    public long? WalletId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal AmountPaid { get; set; }

    public long Quantity { get; set; }

    public string Modeofpayment { get; set; } = null!;

    public string? Status { get; set; }

    public long? ItemId { get; set; }

    public long? MerchantId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Item? Item { get; set; }

    public virtual User? User { get; set; }

    public virtual Wallet? Wallet { get; set; }
}
