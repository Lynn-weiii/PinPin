﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PinPinServer.Models;

public partial class CostCurrencyCategory
{
    public int Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string Icon { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<ScheduleDetail> ScheduleDetails { get; set; } = new List<ScheduleDetail>();

    public virtual ICollection<SplitExpense> SplitExpenses { get; set; } = new List<SplitExpense>();

    public virtual ICollection<Transportation> Transportations { get; set; } = new List<Transportation>();
}