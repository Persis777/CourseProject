using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskTracker.Models;

namespace server.Models
{
    public class AppUser : IdentityUser
    {
          public ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
    }
}