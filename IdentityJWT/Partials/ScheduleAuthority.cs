﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PinPinServer.Models;

public partial class ScheduleAuthority
{
    public int Id { get; set; }

    public int ScheduleId { get; set; }

    public int UserId { get; set; }

    public int AuthorityCategoryId { get; set; }

    public virtual ScheduleAuthorityCategory AuthorityCategory { get; set; }

    public virtual Schedule Schedule { get; set; }

    public virtual User User { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; } // Add this field

}