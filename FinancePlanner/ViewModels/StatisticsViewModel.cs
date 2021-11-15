using System.Collections.Generic;
using FinancePlanner.Models;

namespace FinancePlanner.ViewModels
{
    public class StatisticsViewModel
    {
        public List<Plan> Plans { get; set; }
        public List<Goal> Goals { get; set; }
        public List<Event> Events { get; set; }
        public List<EventCategory> EventCategories { get; set; }
        public Dictionary<string, double?> EventCategoryMap { get; set; }
        public Dictionary<string, double?> SpendingByDateMap { get; set; }
        public Dictionary<string, double?> SpendingByCategoryMap { get; set; }
        public List<FinancialEvent> FinancialEvents { get; set; }
    }
}