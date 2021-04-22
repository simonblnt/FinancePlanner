using System;
using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models.UserSpecific
{
    public class FinancialEvent
    {
        [Key] public int Id { get; set; }
        public int UserId { get; set; }
        public string Subject { get; set; }
        public int Amount { get; set; }
        public int CurrencyId { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Recurring { get; set; }
    }
}