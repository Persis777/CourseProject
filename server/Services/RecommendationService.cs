using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class RecommendationService
    {
        private readonly ApplicationDBContext _context;

        public RecommendationService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task GenerateRecommendationsForUser(string userId)
        {
            // Get tasks from the last week
            var lastWeek = DateTime.UtcNow.AddDays(-7);
            var userTasks = await _context.UserTasks
                .Where(t => t.AppUserId == userId && t.CreatedAt >= lastWeek)
                .Include(t => t.Plan)
                .ThenInclude(p => p.Steps)
                .ToListAsync();

            if (!userTasks.Any()) return;

            // Analyze deadlines
            var overdueTasks = userTasks.Count(t => t.Deadline < DateTime.UtcNow && !t.IsCompleted);
            if (overdueTasks > 0)
            {
                await AddRecommendation(userId, 
                    $"На минулому тижні у вас було {overdueTasks} прострочених завдань. Спробуйте краще планувати свій час та встановлювати більш реалістичні дедлайни.",
                    "Deadline");
            }

            // Analyze task completion rate
            var completedTasks = userTasks.Count(t => t.IsCompleted);
            var totalTasks = userTasks.Count;
            var completionRate = totalTasks > 0 ? (double)completedTasks / totalTasks : 0;
            if (completionRate < 0.5)
            {
                await AddRecommendation(userId,
                    "Ваш рівень виконання завдань нижче 50%. Можливо, варто розбити складні завдання на менші підзавдання для кращого прогресу.",
                    "Productivity");
            }

            // Analyze task distribution
            var tasksPerDay = userTasks
                .GroupBy(t => t.CreatedAt.Date)
                .Select(g => g.Count())
                .ToList();

            if (tasksPerDay.Any())
            {
                var maxTasksPerDay = tasksPerDay.Max();
                var minTasksPerDay = tasksPerDay.Min();
                
                if (maxTasksPerDay - minTasksPerDay > 3)
                {
                    await AddRecommendation(userId,
                        "Ваше навантаження розподілено нерівномірно протягом тижня. Спробуйте краще розподілити завдання для більш збалансованої роботи.",
                        "Balance");
                }
            }

            // Analyze task complexity
            var complexTasks = userTasks.Count(t => t.Plan != null && t.Plan.Steps.Count > 5);
            if (complexTasks > 2)
            {
                await AddRecommendation(userId,
                    "У вас багато складних завдань з великою кількістю кроків. Розгляньте можливість делегування частини завдань або розбиття їх на менші частини.",
                    "Complexity");
            }
        }

        public async Task<List<UserRecommendation>> GetUserRecommendations(string userId)
        {
            return await _context.UserRecommendations
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> MarkRecommendationAsRead(int recommendationId, string userId)
        {
            var recommendation = await _context.UserRecommendations
                .FirstOrDefaultAsync(r => r.Id == recommendationId && r.UserId == userId);

            if (recommendation == null)
            {
                return false;
            }

            recommendation.IsRead = true;
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task AddRecommendation(string userId, string text, string type)
        {
            var recommendation = new UserRecommendation
            {
                UserId = userId,
                RecommendationText = text,
                CreatedAt = DateTime.UtcNow,
                IsRead = false,
                RecommendationType = type
            };

            _context.UserRecommendations.Add(recommendation);
            await _context.SaveChangesAsync();
        }
    }
} 