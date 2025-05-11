using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.Models
{
    public class Plan
    {
       public int Id {get; set;}

       public string Title {get; set;}
        public DateTime CreationDate { get; set; } = DateTime.Now;
       public int UserTaskId { get; set; }       
       public UserTask UserTask { get; set; }

       public List<PlanStep> Steps { get; set;} = new List<PlanStep>();


    }
}