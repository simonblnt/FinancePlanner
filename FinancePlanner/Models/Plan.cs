using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class Plan
    {
        [Key] public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public int EventCategoryId { get; set; }
        public int PlanStatusId { get; set; }
    }
}