using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Dtos.PlanStep;

namespace TaskTracker.Dtos.Plan
{
    public class PlanDto
    {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreationDate { get; set; }

    public ICollection<PlanStepDto> Steps { get; set; }
    }
}