using System;
using FinancePlanner.Models;
using FinancePlanner.Models.Main;
using Microsoft.EntityFrameworkCore;  

namespace FinancePlanner.Database
{
    public class PlannerContext : DbContext
    {
        public PlannerContext(DbContextOptions<PlannerContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 0,
                    Email = "simon.blnt93@gmail.com",
                    FirstName = "admin",
                    LastName = "user",
                    PaidLeavesTotal = 0,
                    PaidLeavesUser = 0,
                    Password = "adminadmin",
                    RegisteredAt = DateTime.Now,
                    UserName = "admin"
                }
            );
            
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 0,
                    Name = "Hungary",
                    ContinentName = "Europe",
                }
            );
            
            modelBuilder.Entity<EventCategory>().HasData(
                new EventCategory
                {
                    Id = 0,
                    Title = "General",
                    IsDayLong = false,
                }
            );
            
            modelBuilder.Entity<GoalStatus>().HasData(
                new GoalStatus
                {
                    Id = 0,
                    Status = "Completed",
                    IsComplete = true,
                },
                new GoalStatus
                {
                    Id = 1,
                    Status = "Failed",
                    IsComplete = false,
                }
            );
            
            modelBuilder.Entity<GoalType>().HasData(
                new GoalType
                {
                    Id = 0,
                    Name = "Percentage",
                    Type = "Numeric",
                }
            );
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<FinancialEvent> FinancialEvents { get; set; }
        public DbSet<FitnessEvent> FitnessEvents { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<GoalStatus> GoalStatuses { get; set; }
        public DbSet<GoalType> GoalTypes { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}