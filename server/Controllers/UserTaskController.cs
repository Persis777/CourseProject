using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Extensions;
using server.Interfaces;
using server.Models;
using TaskTracker.Data;
using TaskTracker.Dtos.UserTask;
using TaskTracker.Mappers;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
      private readonly IUserTaskRepository _userTaskRepo;
      private readonly UserManager<AppUser> _userManager;
        public UserTaskController(IUserTaskRepository userTaskRepo,UserManager<AppUser> userManager)
        {
          _userTaskRepo = userTaskRepo ?? throw new ArgumentNullException(nameof(userTaskRepo));
          _userManager = userManager;
        }

      [HttpGet]
      [Authorize]
      public async  Task<IActionResult> GetUserTasks()
      {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var userTasks = await _userTaskRepo.GetAllUserTasksAsync(appUser);


        return Ok(userTasks);
      }

      [HttpGet]
      [Route("{id}")]
      public async Task<ActionResult<UserTaskDto>> GetUserTaskById([FromRoute] int id)
      {
          var userTask = await _userTaskRepo.GetTaskByIdAsync(id);
          if( userTask == null)
          {
            return NotFound();
          }
          return Ok(userTask);
      }
     [HttpPost]
     [Authorize]
      public async Task<ActionResult> AddTask([FromBody] CreateUserTaskRequestDto userTaskDto)
      {
          if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
         }
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null)
            {
                    return Unauthorized(); 
            }
            userTaskDto.AppUserId = appUser.Id;
            var createdUserTask = await _userTaskRepo.CreateTaskAsync(userTaskDto);

    
          return CreatedAtAction(nameof(GetUserTaskById), new { id = createdUserTask.Id }, createdUserTask);
          }

       [HttpPut]
       [Route("{id}")]
       [Authorize]
       public async Task<ActionResult> UpdateUserTask([FromBody] UpdateUserTaskRequestDto updateDto,[FromRoute] int id)
       {
          
         if (!ModelState.IsValid)
         {
           return BadRequest(ModelState);
         }
          var username = User.GetUsername();
          var appUser = await _userManager.FindByNameAsync(username);
          var result = await _userTaskRepo.UpdateTaskAsync(updateDto,id, appUser);

          if (result == null)
          {
            return NotFound();
          }


          return NoContent();
       }

       [HttpDelete]
       [Route("{id}")]

       public async Task<IActionResult> DeleteUserTask([FromRoute] int id)
       {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);

         var result = await _userTaskRepo.DeleteTaskAsync(id, appUser.Id);

         if(result)
         {
          return Ok();
         }
         else
         {
          return NotFound();
         }         
       }
    }
}
