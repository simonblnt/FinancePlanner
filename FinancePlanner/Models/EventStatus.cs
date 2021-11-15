using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class EventStatus
    {
        [Key] public int Id { get; set; }
        public string Status { get; set; }
    }
}