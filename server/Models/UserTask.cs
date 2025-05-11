using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.Models
{
    public class UserTask
    {
       public int Id { get; set; }

       public string Title { get; set; }

       public string Description { get; set; } = string.Empty;

       public DateTime Deadline { get; set; }

       public string Priority { get; set; }

       public string Status { get; set;}

       public Plan Plan { get; set;}

       public int? PlanId { get; set; }

       public string AppUserId { get; set; }

       public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

       public bool IsCompleted { get; set; }
    }
}