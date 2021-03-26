using System;

namespace FinancePlanner.Models.UserSpecific
{
    public class Note
    {
        private int Id { get; set; }
        private int UserId { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private DateTime DateAdded { get; set; }
        private int EventId { get; set; }
        private int FinancialEventId { get; set; }
        private int ContactId { get; set; }
    }
}