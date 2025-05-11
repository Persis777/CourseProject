using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Plan;
using Microsoft.EntityFrameworkCore;
using server.Interfaces;
using TaskTracker.Data;
using TaskTracker.Dtos.Plan;
using TaskTracker.Mappers;
using TaskTracker.Models;

namespace server.Repository
{
    public class PlanRepository : IPlanRepository
    {
        private readonly ApplicationDBContext _context;
        public PlanRepository(ApplicationDBContext context)
        {
          _context = context;  
        }
        public async Task<PlanDto> CreatePlanAsync(CreatePlanRequestDto createPlanRequest)
        {
            var userTask = await _context.UserTasks
            .Include(ut => ut.Plan)
            .FirstOrDefaultAsync(ut => ut.Id == createPlanRequest.UserTaskId);

            if (userTask == null)
            {
                return null;
            }
            if (userTask.Plan != null)
            {
                return null;
            }
            
            var newPlan = createPlanRequest.ToPlanFromCreateDto();
            newPlan.UserTaskId = createPlanRequest.UserTaskId;

            _context.Plans.Add(newPlan);
            await _context.SaveChangesAsync();

            userTask.PlanId = newPlan.Id;
            _context.UserTasks.Update(userTask);

            await _context.SaveChangesAsync();

            return newPlan.ToPlanDto();
        }

        public async Task<bool> DeletePlanAsync(int id)
        {
            var plan = await _context.Plans.FindAsync(id);

            if(plan == null)
            {
                return false;
            }
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PlanDto> GetPlanByUserTaskIdAsync(int userTaskId)
        {
            var plan = await _context.Plans
            .Include(p => p.Steps)
            .FirstOrDefaultAsync(p => p.UserTaskId == userTaskId);

            if(plan == null)
            {
                return null;
            }
           return plan?.ToPlanDto();
           
        }

        public async Task<PlanDto> UpdatePlanAsync(int id, UpdatePlanRequestDto updatePlanRequestDto)
        {
            var plan = await _context.Plans
            .Include(p => p.Steps)
            .FirstOrDefaultAsync(p => p.Id == id);

            if(plan == null)
            {
                return null;
            }
            if(!string.IsNullOrEmpty(updatePlanRequestDto.Title))
            {
                plan.Title = updatePlanRequestDto.Title;
            }
            if(updatePlanRequestDto.Steps != null && updatePlanRequestDto.Steps.Any())
            {
                plan.Steps.Clear();
                plan.Steps = updatePlanRequestDto.Steps.Select(stepDto => stepDto.ToPlanStepFromUpdateDto()).ToList();
            }
             await _context.SaveChangesAsync();

             return plan.ToPlanDto();
        }
    }
}