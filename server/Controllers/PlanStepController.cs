using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.PlanStep;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using server.Interfaces;
using TaskTracker.Dtos.PlanStep;
using TaskTracker.Mappers;

namespace server.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/PlanStep")]
    [ApiController]
    public class PlanStepController : ControllerBase
    {
        private readonly IPlanStepRepository _planStepRepo;

        public PlanStepController(IPlanStepRepository planStepRepo)
        {
            _planStepRepo = planStepRepo;
        }

        // GET: api/PlanStep/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanStepDto>> GetPlanStepById(int id)
        {
            var planStepDto = await _planStepRepo.GetStepByIdAsync(id);

            if (planStepDto == null)
            {
                return NotFound($"Plan step with id {id} not found.");
            }

            return Ok(planStepDto);
        }

        // GET: api/PlanStep/plan/{planId}
        [HttpGet("plan/{planId}")]
        public async Task<ActionResult<IEnumerable<PlanStepDto>>> GetStepsByPlanId(int planId)
        {
            var steps = await _planStepRepo.GetStepsByPlanIdAsync(planId);

            if (steps == null || !steps.Any())
            {
                return NotFound($"No steps found for plan with id {planId}.");
            }

            return Ok(steps);
        }

        // POST: api/PlanStep
        [HttpPost]
        public async Task<ActionResult<PlanStepDto>> CreatePlanStep([FromBody] CreatePlanStepRequestDto createStepDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdStep = await _planStepRepo.CreateStepAsync(createStepDto);
            return CreatedAtAction(nameof(GetPlanStepById), new { id = createdStep.Id }, createdStep);
        }

        // PUT: api/PlanStep/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlanStep(int id, [FromBody] UpdatePlanStepRequestDto updateStepDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedStep = await _planStepRepo.UpdateStepAsync(id, updateStepDto);

            if (updatedStep == null)
            {
                return NotFound($"Plan step with id {id} not found.");
            }

            return NoContent();
        }

        // DELETE: api/PlanStep/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlanStep(int id)
        {
            var deleted = await _planStepRepo.DeleteStepAsync(id);

            if (!deleted)
            {
                return NotFound($"Plan step with id {id} not found.");
            }

            return NoContent();
        }

        
    }
}