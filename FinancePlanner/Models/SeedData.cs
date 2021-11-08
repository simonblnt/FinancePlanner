using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using FinancePlanner.Database;
using Npgsql;

namespace FinancePlanner.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PlannerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PlannerContext>>()))
            {
                // Look for any events.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }
                context.Users.AddRange(
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
                
                if (context.Countries.Any())
                {
                    return;   // DB has been seeded
                }
                context.Countries.AddRange(
                    new Country
                    {
                        Id = 0,
                        Name = "Hungary",
                        ContinentName = "Europe",
                    }
                );
                
                if (context.EventCategories.Any())
                {
                    return;   // DB has been seeded
                }
                context.EventCategories.AddRange(
                    new EventCategory
                    {
                        Id = 0,
                        CategoryTitle = "General",
                        IsDayLong = false
                    }
                );
                
                if (context.GoalStatuses.Any())
                {
                    return;   // DB has been seeded
                }
                context.GoalStatuses.AddRange(
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
                
                if (context.GoalTypes.Any())
                {
                    return;   // DB has been seeded
                }

                context.GoalTypes.AddRange(
                    new GoalType
                    {
                        Id = 0,
                        Name = "Percentage",
                        Type = "Numeric",
                    }
                );
                
                // Look for any events.
                if (context.Events.Any())
                {
                    return;   // DB has been seeded
                }
                context.Events.AddRange(
                    new Event
                    {
                        Id = 0,
                        UserId = 0,
                        EventCategoryId = 0,
                        Title = "Test event",
                        Description = "Test description",
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue,
                        IsRecurring = false,
                        CreatedAt = DateTime.Now
                    }
                );
                
                
                
                
                
                context.SaveChanges();
            }
        }
    }
}