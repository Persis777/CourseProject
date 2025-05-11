using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Dtos.PlanStep;

namespace TaskTracker.Dtos.Plan
{
    public class CreatePlanRequestDto
    {
    public string Title { get; set; }
    public DateTime CreationDate { get; set; }
    public int UserTaskId { get; set;}
    public ICollection<CreatePlanStepRequestDto> Steps { get; set; }
    }
}