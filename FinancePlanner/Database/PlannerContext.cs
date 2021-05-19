using System;
using FinancePlanner.Models;
using FinancePlanner.Models.Main;
using FinancePlanner.Models.UserSpecific;
using Microsoft.EntityFrameworkCore;  

namespace FinancePlanner.Database
{
    public class PlannerContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
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
        }
        public PlannerContext(DbContextOptions<PlannerContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactEmail> ContactEmails { get; set; }
        public DbSet<ContactPhone> ContactPhones { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<FinancialEvent> FinancialEvents { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Stat> Stats { get; set; }
        public DbSet<TrackedActivity> TrackedActivities { get; set; }

        
    }
}