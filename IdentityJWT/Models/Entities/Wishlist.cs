﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PinPinServer.Models;

public partial class Wishlist
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<LocationCategory> LocationCategories { get; set; } = new List<LocationCategory>();

    public virtual User User { get; set; }

    public virtual ICollection<WishlistDetail> WishlistDetails { get; set; } = new List<WishlistDetail>();
}