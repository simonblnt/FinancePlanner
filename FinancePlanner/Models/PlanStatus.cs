using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class PlanStatus
    {
        [Key] public int Id { get; set; }
        public string Status { get; set; }
    }
}