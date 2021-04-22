using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models.Main
{
    public class EventType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ForcedDaylong { get; set; }
    }
}