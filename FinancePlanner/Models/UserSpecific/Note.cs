using System;
using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models.UserSpecific
{
    public class Note
    {
        [Key] public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public int EventId { get; set; }
        public int FinancialEventId { get; set; }
        public int ContactId { get; set; }
    }
}