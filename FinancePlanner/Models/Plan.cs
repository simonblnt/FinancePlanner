using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class Plan
    {
        [Key] public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
    }
}