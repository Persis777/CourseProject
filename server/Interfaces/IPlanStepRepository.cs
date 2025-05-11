using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.PlanStep;
using TaskTracker.Dtos.PlanStep;
using TaskTracker.Models;

namespace server.Interfaces
{
    public interface IPlanStepRepository
    {
    Task<PlanStepDto> GetStepByIdAsync(int id);
    Task<IEnumerable<PlanStepDto>> GetStepsByPlanIdAsync(int planId);
    Task<PlanStepDto> CreateStepAsync(CreatePlanStepRequestDto createStepDto);
    Task<PlanStepDto> UpdateStepAsync(int id, UpdatePlanStepRequestDto updateStepDto);
    Task<bool> DeleteStepAsync(int id);

     
    }
}