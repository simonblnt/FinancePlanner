using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class GoalType
    {
        [Key] public int Id { get; set; }
        public int Title { get; set; }
        public string Name { get; set; }
    }
}