using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Plan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Interfaces;
using TaskTracker.Data;
using TaskTracker.Dtos.Plan;
using TaskTracker.Mappers;

namespace backend.Controllers
{
    [Route("api/Plan")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanRepository _planRepo;  
        private readonly ApplicationDBContext _context;      
       public PlanController(IPlanRepository planRepo,ApplicationDBContext context)
       {
          _planRepo = planRepo;
          _context = context;
       }

       [HttpGet("user/{userTaskId}")]
       public async Task<ActionResult<PlanDto>> GetPlanByUserTaskId([FromRoute] int userTaskId)
       {
          var planDto = await _planRepo.GetPlanByUserTaskIdAsync(userTaskId);

          if (planDto == null)
          {
            return NotFound($"Not plan found for UserTaskId: {userTaskId}");
          }
          return Ok(planDto);
       }

       [HttpPost]
       public async Task<ActionResult<PlanDto>> CreatePlan([FromBody] CreatePlanRequestDto createPlanRequest)
       {
          if(!ModelState.IsValid)
          {
            return BadRequest(ModelState);
          }

          var planDto = await _planRepo.CreatePlanAsync(createPlanRequest);

          if (planDto == null)
          {
            var userTask = await _context.UserTasks.FindAsync(createPlanRequest.UserTaskId);
            if (userTask?.Plan != null)
            {
              return Conflict(new { Message = "UserTask already has a plan"});
            }

            return NotFound(new { Message = "UserTask not found, or it's already has a plan"});
          }

          return CreatedAtAction(nameof(GetPlanByUserTaskId),new {userTaskId = createPlanRequest.UserTaskId}, planDto);
       }
       
       [HttpPut]
       [Route("{id}")]

       public async Task<ActionResult> UpdatePlan([FromRoute] int id,[FromBody] UpdatePlanRequestDto updatePlanRequestDto)
       {
          if(!ModelState.IsValid)
          {
            return BadRequest(ModelState);
          }

          var planDto = await _planRepo.UpdatePlanAsync(id,updatePlanRequestDto);

          if (planDto == null)
          {
            return NotFound();
          }

          return Ok(planDto);

       }

       [HttpDelete]
       [Route("{id}")]

       public async Task<ActionResult> DeletePlan([FromRoute] int id)
       {
          var success = await _planRepo.DeletePlanAsync(id);

          if (!success)
          {
            return NotFound();
          }

          return NoContent();
       }
    
    }
}