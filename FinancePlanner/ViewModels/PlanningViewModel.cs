using System.Collections.Generic;
using FinancePlanner.Models;

namespace FinancePlanner.ViewModels
{
    public class PlanningViewModel
    {
        public List<Plan> Plans { get; set; }
        public List<Goal> Goals { get; set; }
        
        public List<GoalType> GoalTypes { get; set; }
        public List<EventCategory> EventCategories { get; set; }
        public List<GoalStatus> GoalStatuses { get; set; }

        public List<Event> Events { get; set; }
        
        // Plan
        public int PlanId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public int EventCategoryId { get; set; }
        
        // Goal
        public int GoalId { get; set; }
        public int GoalPlanId { get; set; }
        public int GoalTypeId { get; set; }
        public int EventId { get; set; }
        public int GoalStatusId { get; set; }
        public string GoalTitle { get; set; }
        public int NumericalTarget { get; set; }
        public int NumericalProgress { get; set; }
        
        // Goal Status
        public int StatusId { get; set; }
        public string Status { get; set; }
        public bool IsComplete { get; set; }
        
        // Goal Type
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string Type { get; set; }
        
        
    }
}