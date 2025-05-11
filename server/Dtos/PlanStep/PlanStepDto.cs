using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.Dtos.PlanStep
{
    public class PlanStepDto
    {
        public int Id { get; set;}
        public int PlanId { get; set; }

         public string Text{ get; set; }

         public int Order {get; set;}
    }
}