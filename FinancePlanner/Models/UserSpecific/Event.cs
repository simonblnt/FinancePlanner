using System;
using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models.UserSpecific
{
    public class Event
    {
        [Key] public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EventTypeId { get; set; }
    }
}