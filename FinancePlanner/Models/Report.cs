using System;
using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class Report
    {
        [Key] public int Id { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        public int PlantId { get; set; }
        public string Title { get; set; }
        public string SendingSchedule { get; set; }
        public DateTime NextScheduledDate { get; set; }
        public int ScheduleWeekday { get; set; }
        public int ScheduleDay { get; set; }
        public int ScheduleMonth { get; set; }
        public int ScheduleMonthlyDay { get; set; }
        public int CreatedAt { get; set; }
    }
}