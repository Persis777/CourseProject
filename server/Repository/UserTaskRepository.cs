using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Interfaces;
using server.Models;
using TaskTracker.Data;
using TaskTracker.Dtos.UserTask;
using TaskTracker.Mappers;
using TaskTracker.Models;

namespace server.Repository
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly ApplicationDBContext _context;

        public UserTaskRepository(ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<UserTaskDto> CreateTaskAsync(CreateUserTaskRequestDto userTaskDto)
        {   
            var userTask = userTaskDto.ToUserTaskFromCreateDto();     
            _context.UserTasks.Add(userTask);
           
            await _context.SaveChangesAsync();
            return userTask.ToUserTaskDto();
        }

        public async Task<bool> DeleteTaskAsync(int taskId, string appUserId)
        {
           var userTask = await _context.UserTasks
           .Where(ut => ut.Id == taskId && ut.AppUserId == appUserId)
           .FirstOrDefaultAsync();

           if(userTask == null)
           {
            return false;
           }  
           _context.UserTasks.Remove(userTask);
           await _context.SaveChangesAsync();

           return true;
        }

        public async Task<IEnumerable<UserTaskDto>> GetAllUserTasksAsync(AppUser user)
        {
            return await _context.UserTasks
                .Where(u => u.AppUserId == user.Id)
               .Include(ut => ut.Plan)
               .ThenInclude(p => p.Steps)
               .Select(s => s.ToUserTaskDto())
               .ToListAsync();
            
        }

        public async Task<UserTaskDto> GetTaskByIdAsync(int id)
        {
            var userTask = await _context.UserTasks 
               .Include(ut => ut.Plan)
               .ThenInclude(p => p.Steps)
               .FirstOrDefaultAsync(ut => ut.Id == id);

            return userTask?.ToUserTaskDto();
        }

        public async Task<UserTaskDto> UpdateTaskAsync(UpdateUserTaskRequestDto updateDto,int taskId, AppUser user)
        {
            var userTask = await _context.UserTasks
            .Where(ut => ut.Id == taskId && ut.AppUserId == user.Id)
            .FirstOrDefaultAsync();

            if(userTask == null)
            {
                return null;
            }

            userTask.Title = updateDto.Title;
            userTask.Description = updateDto.Description;
            userTask.Deadline = updateDto.Deadline;
            userTask.Priority = updateDto.Priority;
            userTask.Status = updateDto.Status;

            await _context.SaveChangesAsync();

            return userTask.ToUserTaskDto();
        }
    }
}