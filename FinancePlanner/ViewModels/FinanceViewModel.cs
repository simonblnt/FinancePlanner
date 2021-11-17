using System.Collections.Generic;
using System.Security.Permissions;
using FinancePlanner.Models;

namespace FinancePlanner.ViewModels
{
    public class FinanceViewModel
    {
        public List<Event> Events { get; set; }
        public List<FinancialEvent> FinancialEvents { get; set; }
        public List<EventCategory> EventCategories { get; set; }
        public List<Wallet> Wallets { get; set; }
    }
}