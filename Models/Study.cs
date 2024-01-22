using System;
using System.Collections.Generic;

namespace ProgressPacer.Models;

public partial class Study
{
    public int StudyId { get; set; }

    public string ModuleCode { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public decimal TotalSelfStudy { get; set; }

    public decimal SelfStudyHours { get; set; }

    public DateOnly StudyDate { get; set; }

    public decimal RemainingHrs { get; set; }

    public int MId { get; set; }

    public string Username { get; set; } = null!;

    public virtual Module MIdNavigation { get; set; } = null!;
}
