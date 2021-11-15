using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FinancePlanner.Models
{
    public class Event
    {
        [Key] public int Id { get; set; }
        public string UserId { get; set; }
        public int FinancialEventId { get; set; }
        public int FitnessEventId { get; set; }
        public int EventCategoryId { get; set; }
        public int EventStatusId { get; set; }
        public int GoalTypeId { get; set; }
        public int GoalId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}