using System;
using System.Collections.Generic;

namespace ProgressPacer.Models;

public partial class Module
{
    public int MId { get; set; }

    public string ModuleCode { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public int NumCredits { get; set; }

    public decimal NumWeeks { get; set; }

    public decimal ClassHours { get; set; }

    public decimal SelfStudy { get; set; }

    public DateOnly StartDate { get; set; }

    public string Username { get; set; } = null!;

    public virtual ICollection<Study> Studies { get; set; } = new List<Study>();
}
