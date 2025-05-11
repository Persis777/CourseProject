using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Dtos.UserTask;
using TaskTracker.Models;

namespace TaskTracker.Mappers
{
    public static class UserTaskMappers
    {
      public static UserTaskDto ToUserTaskDto(this UserTask userTaskModel)
      {
        if (userTaskModel == null)
        {
           return null;
        }
        var planDto = userTaskModel.Plan?.ToPlanDto();
        return new UserTaskDto
        {
          Id = userTaskModel.Id, 
          Title = userTaskModel.Title,
          Description = userTaskModel.Description,
          Deadline = userTaskModel.Deadline,
          Priority = userTaskModel.Priority,
          Status = userTaskModel.Status,
          Plan = planDto,
        
        };
      }

      public static UserTask ToUserTaskFromCreateDto(this CreateUserTaskRequestDto userTaskDto)
      {
        return new UserTask
        {
           Title = userTaskDto.Title,
           Description = userTaskDto.Description,
           Deadline = userTaskDto.Deadline,
           Priority = userTaskDto.Priority,
           Status = userTaskDto.Status,
           AppUserId = userTaskDto.AppUserId,
          // Plan = userTaskDto.Plan?.ToPlanFromCreateDto()
        };
      }
    }
}