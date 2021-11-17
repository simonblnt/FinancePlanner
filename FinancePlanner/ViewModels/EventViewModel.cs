using System;
using System.Collections.Generic;
using FinancePlanner.Models;

namespace FinancePlanner.ViewModels
{
    public class EventViewModel
    {
        public List<Event> Events { get; set; }
        public List<EventCategory> EventCategories { get; set; }
        public List<EventStatus> EventStatuses { get; set; }
        public List<GoalType> GoalTypes { get; set; }
        
        public Event NewEvent { get; set; }
    }
}