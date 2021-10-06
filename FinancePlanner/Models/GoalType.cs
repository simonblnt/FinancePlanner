using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class GoalType
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}