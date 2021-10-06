using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class GoalStatus
    {
        [Key] public int Id { get; set; }
        public string Status { get; set; }
        public bool IsComplete { get; set; }
    }
}