using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using server.Models;
using TaskTracker.Models;

namespace TaskTracker.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanStep> PlanSteps { get; set; }
        public DbSet<UserRecommendation> UserRecommendations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                 new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            // Configure UserTask-Plan relationship
            modelBuilder.Entity<Plan>()
                .HasOne(p => p.UserTask)
                .WithOne(t => t.Plan)
                .HasForeignKey<Plan>(p => p.UserTaskId);

            // Configure Plan-PlanStep relationship
            modelBuilder.Entity<PlanStep>()
                .HasOne(ps => ps.Plan)
                .WithMany(p => p.Steps)
                .HasForeignKey(ps => ps.PlanId);

            // Configure User-Recommendation relationship
            modelBuilder.Entity<UserRecommendation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);
        }
    }
}
