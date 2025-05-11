using System;
using System.ComponentModel.DataAnnotations;
using server.Models;

namespace TaskTracker.Models
{
    public class UserRecommendation
    {
        [Key]
        public int Id { get; set; }
        
        public string UserId { get; set; }
        public AppUser User { get; set; }
        
        public string RecommendationText { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public bool IsRead { get; set; }
        
        public string RecommendationType { get; set; } // "Deadline", "Productivity", "Balance", etc.
    }
} 