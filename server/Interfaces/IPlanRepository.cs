using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Plan;
using TaskTracker.Dtos.Plan;

namespace server.Interfaces
{
    public interface IPlanRepository
    {
        Task<PlanDto> GetPlanByUserTaskIdAsync(int userTaskId);
        Task<PlanDto> CreatePlanAsync(CreatePlanRequestDto createPlanRequest);
        Task<PlanDto> UpdatePlanAsync(int id, UpdatePlanRequestDto updatePlanRequestDto);
        Task<bool> DeletePlanAsync(int id);
    }
}