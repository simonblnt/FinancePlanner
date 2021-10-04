using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class FinancialEvent
    {
        // ** Default currency is in Euros (€) **
        [Key] public int Id { get; set; }
        public int Amount { get; set; }
        public string Subject { get; set; }
    }
}