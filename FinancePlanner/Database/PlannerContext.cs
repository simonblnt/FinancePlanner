using System;
using FinancePlanner.Models;
using FinancePlanner.Models.Main;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;  

namespace FinancePlanner.Database
{
    public class PlannerContext : IdentityDbContext
    {
        public PlannerContext(DbContextOptions<PlannerContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<FinancialEvent> FinancialEvents { get; set; }
        public DbSet<FitnessEvent> FitnessEvents { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<GoalStatus> GoalStatuses { get; set; }
        public DbSet<PlanStatus> PlanStatuses { get; set; }
        public DbSet<GoalType> GoalTypes { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<IdentityUser> Users { get; set; }
    }
}