using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Dtos.Plan;
using TaskTracker.Dtos.PlanStep;
using TaskTracker.Models;

namespace TaskTracker.Dtos.UserTask
{
    public class UserTaskDto
    {
       public int Id { get; set; }

       public string Title { get; set; }

       public string Description { get; set; } = string.Empty;

       public DateTime Deadline { get; set; }

       public string Priority { get; set; }

       public string Status { get; set;}

       public PlanDto Plan { get; set;}

      // public int PlanId { get; set; }
       
    }
}