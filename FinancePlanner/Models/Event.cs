using System;
using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class Event
    {
        [Key] public int Id { get; set; }
        public int UserId { get; set; }
        public int FinancialEventId { get; set; }
        public int FitnessEventId { get; set; }
        public int EventCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}