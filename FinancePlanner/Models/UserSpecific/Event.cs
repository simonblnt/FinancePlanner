using System;

namespace FinancePlanner.Models.UserSpecific
{
    public class Event
    {
        private int Id { get; set; }
        private int UserId { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }
        private int EventTypeId { get; set; }
    }
}