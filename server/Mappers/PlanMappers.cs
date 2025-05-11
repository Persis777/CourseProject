using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Dtos.Plan;
using TaskTracker.Dtos.PlanStep;
using TaskTracker.Models;

namespace TaskTracker.Mappers
{
    public static class PlanMappers
    {
       public static PlanDto ToPlanDto(this Plan planModel)
       {
         return new PlanDto
         {
            Id = planModel.Id,
            Title = planModel.Title,
            CreationDate = planModel.CreationDate,
            Steps = planModel.Steps?.Select(step => new PlanStepDto
            {
              PlanId = step.PlanId,
              Id = step.Id,           
              Text =  step.Text,
              Order = step.Order
            }).ToList()
         };
       }
       public static Plan ToPlanFromCreateDto(this CreatePlanRequestDto planDto)
       {
         return new Plan
         {
             Title = planDto.Title,
             CreationDate = planDto.CreationDate,
             Steps = planDto.Steps?.Select (stepDto => stepDto.ToPlanStepFromCreateDto()).ToList()
         };
       }
    }
}