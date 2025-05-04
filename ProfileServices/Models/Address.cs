using System;
using System.Collections.Generic;

namespace ProfileServices.Models;

public partial class Address
{
    public long AddressId { get; set; }

    public long UserId { get; set; }

    public string HouseNumber { get; set; } = null!;

    public string StreetName { get; set; } = null!;

    public string? ColonyName { get; set; }

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Pincode { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
