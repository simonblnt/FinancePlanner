using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class EventCategory
    {
        [Key] public int Id { get; set; }
        public string CategoryTitle { get; set; }
        public bool IsDayLong { get; set; }
    }
}