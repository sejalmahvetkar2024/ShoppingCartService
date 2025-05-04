using System;
using System.Collections.Generic;

namespace CartService.Models;

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

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
