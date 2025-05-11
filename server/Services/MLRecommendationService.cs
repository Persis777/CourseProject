using Microsoft.ML;
using Microsoft.ML.Trainers;
using Microsoft.ML.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class MLRecommendationService
    {
        private readonly ApplicationDBContext _context;
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public MLRecommendationService(ApplicationDBContext context)
        {
            _context = context;
            _mlContext = new MLContext(seed: 1);
        }

        public class TaskInteraction
        {
            [LoadColumn(0)]
            public string UserId { get; set; }
            
            [LoadColumn(1)]
            public int TaskId { get; set; }
            
            [LoadColumn(2)]
            public float Rating { get; set; }
            
            [LoadColumn(3)]
            public DateTime Timestamp { get; set; }
        }

        public class TaskPrediction
        {
            public float Score { get; set; }
        }

        public async Task TrainModel()
        {
            // Get user task interactions
            var interactions = await GetUserTaskInteractions();

            // Create training data
            var trainingData = _mlContext.Data.LoadFromEnumerable(interactions);

            // Define pipeline
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("UserId")
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("TaskId"))
                .Append(_mlContext.Transforms.Concatenate("Features", "UserId", "TaskId"))
                .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("UserId"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("TaskId"))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                    labelColumnName: "Rating",
                    featureColumnName: "Features"));

            // Train model
            _model = pipeline.Fit(trainingData);
        }

        private async Task<List<TaskInteraction>> GetUserTaskInteractions()
        {
            var interactions = new List<TaskInteraction>();
            var tasks = await _context.UserTasks
                .Include(t => t.Plan)
                .ThenInclude(p => p.Steps)
                .ToListAsync();

            foreach (var task in tasks)
            {
                // Calculate rating based on task completion and complexity
                float rating = CalculateTaskRating(task);
                
                interactions.Add(new TaskInteraction
                {
                    UserId = task.AppUserId,
                    TaskId = task.Id,
                    Rating = rating,
                    Timestamp = task.CreatedAt
                });
            }

            return interactions;
        }

        private float CalculateTaskRating(UserTask task)
        {
            float rating = 0.5f; // Base rating

            // Adjust rating based on task completion
            if (task.IsCompleted)
            {
                rating += 0.3f;
            }

            // Adjust rating based on task complexity
            if (task.Plan?.Steps != null)
            {
                var complexity = task.Plan.Steps.Count;
                rating += Math.Min(complexity * 0.1f, 0.2f);
            }

            // Adjust rating based on deadline adherence
            if (task.Deadline > DateTime.UtcNow)
            {
                rating += 0.2f;
            }

            return Math.Min(rating, 1.0f);
        }

        public async Task<List<UserTask>> GetRecommendedTasks(string userId, int count = 5)
        {
            if (_model == null)
            {
                await TrainModel();
            }

            var allTasks = await _context.UserTasks.ToListAsync();
            var predictions = new List<(UserTask Task, float Score)>();

            foreach (var task in allTasks)
            {
                var predictionEngine = _mlContext.Model.CreatePredictionEngine<TaskInteraction, TaskPrediction>(_model);
                var prediction = predictionEngine.Predict(new TaskInteraction
                {
                    UserId = userId,
                    TaskId = task.Id,
                    Rating = 0
                });

                predictions.Add((Task: task, Score: prediction.Score));
            }

            return predictions
                .OrderByDescending(p => p.Score)
                .Take(count)
                .Select(p => p.Task)
                .ToList();
        }
    }
} 