using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.PlanStep;
using Microsoft.EntityFrameworkCore;
using server.Interfaces;
using TaskTracker.Data;
using TaskTracker.Dtos.PlanStep;
using TaskTracker.Mappers;

namespace server.Repository
{
    public class PlanStepRepository : IPlanStepRepository
    {
private readonly ApplicationDBContext _context;

    public PlanStepRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<PlanStepDto> GetStepByIdAsync(int id)
    {
       // var step = await _context.PlanSteps.FindAsync(id);
       // return step?.ToPlanStepDto();

    var step = await _context.PlanSteps
        .Where(ps => ps.Id == id)
        .Select(ps => new PlanStepDto
        {
            Id = ps.Id,
            PlanId = ps.PlanId,
            Text = ps.Text,
            Order = ps.Order
        })
        .FirstOrDefaultAsync();

    return step;
    }

    public async Task<IEnumerable<PlanStepDto>> GetStepsByPlanIdAsync(int planId)
    {
        var steps = await _context.PlanSteps
            .Where(s => s.PlanId == planId)
            .ToListAsync();

        return steps.Select(s => s.ToPlanStepDto());
    }

   public async Task<PlanStepDto> CreateStepAsync(CreatePlanStepRequestDto createStepDto)
     {
        var planExists = await _context.Plans.AnyAsync(p => p.Id == createStepDto.PlanId);
        if (!planExists)
           {
               throw new ArgumentException($"Plan with id {createStepDto.PlanId} does not exist.");
           }
    
           var planSteps = await _context.PlanSteps
           .Where(s => s.PlanId == createStepDto.PlanId)
           .OrderBy(s => s.Order)
           .ToListAsync();

           foreach(var step in planSteps.Where( s => s.Order >= createStepDto.Order))
           {
             step.Order++;
           }

           var planStep = createStepDto.ToPlanStepFromCreateDto();
           planStep.PlanId = createStepDto.PlanId;
           planStep.Order = createStepDto.Order;  

          _context.PlanSteps.Add(planStep);
          await _context.SaveChangesAsync();

          return planStep.ToPlanStepDto();
     }

    public async Task<PlanStepDto> UpdateStepAsync(int id, UpdatePlanStepRequestDto updateStepDto)
{
    var planStep = await _context.PlanSteps.FindAsync(id);

    if (planStep == null)
    {
        throw new ArgumentException($"Plan step with id {id} not found.");
    }
    
    var planSteps = await _context.PlanSteps
    .Where(s => s.PlanId == planStep.PlanId)
    .OrderBy(s => s.Order)
    .ToListAsync();

    if(updateStepDto.Order != planStep.Order)
    {
        if(updateStepDto.Order < planStep.Order)
        {
            foreach (var step in planSteps.Where(s => s.Order >= updateStepDto.Order && s.Order < planStep.Order))
            {
                step.Order++;
            }
        }

        else if (updateStepDto.Order > planStep.Order)
        {
            foreach(var step in planSteps.Where(s => s.Order > planStep.Order && s.Order <= updateStepDto.Order))
            {
                step.Order--;
            }
        }
    }


    planStep.Text = updateStepDto.Text;
    planStep.Order = updateStepDto.Order;

    _context.PlanSteps.Update(planStep);
    await _context.SaveChangesAsync();

    
    return planStep.ToPlanStepDto();
}


    public async Task<bool> DeleteStepAsync(int id)
    {
        var planStep = await _context.PlanSteps.FindAsync(id);

        if (planStep == null)
        {
            return false;
        }
         _context.PlanSteps.Remove(planStep);

         var planSteps = await _context.PlanSteps
         .Where(s => s.PlanId == planStep.PlanId && s.Order > planStep.Order)
         .ToListAsync();

         foreach(var step in planSteps)
         {
            step.Order--;
         }
            await _context.SaveChangesAsync();
        return true;
    }

    
    }
}