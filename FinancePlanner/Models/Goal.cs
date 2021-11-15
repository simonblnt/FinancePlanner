using System;
using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class Goal
    {
        [Key] public int Id { get; set; }
        public int PlanId { get; set; }
        public int GoalTypeId { get; set; }
        public int EventId { get; set; }
        public int GoalStatusId { get; set; }
        public bool GlobalByCategory { get; set; }
        public DateTime Deadline { get; set; }
        public string Title { get; set; }
        public int NumericalTarget { get; set; }
        public int NumericalProgress { get; set; }
    }
}