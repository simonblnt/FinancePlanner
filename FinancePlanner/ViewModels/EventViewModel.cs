using System;
using System.Collections.Generic;
using FinancePlanner.Models;

namespace FinancePlanner.ViewModels
{
    public class EventViewModel
    {
        public List<Event> Events { get; set; }
        public List<EventCategory> EventCategories { get; set; }
    }
}