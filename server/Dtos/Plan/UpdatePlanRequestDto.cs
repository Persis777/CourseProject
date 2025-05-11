using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.PlanStep;

namespace backend.Dtos.Plan
{
    public class UpdatePlanRequestDto
    {
     public string Title { get; set; }

     public List<UpdatePlanStepRequestDto> Steps { get; set;}
     
    }
}