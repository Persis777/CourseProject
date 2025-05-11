using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.Dtos.UserTask
{
    public class UpdateUserTaskRequestDto
    {
       public string Title { get; set; }

       public string Description { get; set; } = string.Empty;

       public DateTime Deadline { get; set; }

       public string Priority { get; set; }

       public string Status { get; set;}

     
    }
}