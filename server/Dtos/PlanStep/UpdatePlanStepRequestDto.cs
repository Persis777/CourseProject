using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.PlanStep
{
    public class UpdatePlanStepRequestDto
    {
      

        public string Text{ get; set; }

        public int Order {get; set;}
    }
}