using System;

namespace FinancePlanner.Models.UserSpecific
{
    public class FinancialEvent
    {
        private int Id { get; set; }
        private int UserId { get; set; }
        private string Subject { get; set; }
        private int Amount { get; set; }
        private int CurrencyId { get; set; }
        private DateTime DateAdded { get; set; }
        private bool Recurring { get; set; }
    }
}