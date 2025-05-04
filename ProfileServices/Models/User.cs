using System;
using System.Collections.Generic;

namespace ProfileServices.Models;

public partial class User
{
    public long UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string PasswordUser { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Merchant? Merchant { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Statement> Statements { get; set; } = new List<Statement>();

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
