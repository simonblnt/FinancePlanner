using System.Collections.Generic;
using FinancePlanner.Models;

namespace FinancePlanner.ViewModels
{
    public class ReportViewModel
    {
        public List<Report> Reports { get; set; }
        public List<Event> Events { get; set; }
        public List<Plan> Plans { get; set; }
        public Event Event { get; set; }
        public Plan Plan { get; set; }
    }
}