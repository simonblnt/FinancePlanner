using System.Collections.Generic;
using FinancePlanner.Models;

namespace FinancePlanner.ViewModels
{
    public class StatisticsViewModel
    {
        public List<Plan> Plans { get; set; }
        public List<Goal> Goals { get; set; }

        public List<EventCategory> EventCategories { get; set; }
    }
}