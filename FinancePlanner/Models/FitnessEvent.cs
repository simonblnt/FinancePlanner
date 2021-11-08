using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class FitnessEvent
    {
        // ** All formats are in SI **
        [Key] public int Id { get; set; }
        public int Distance { get; set; }       // meters
        public int Duration { get; set; }    // seconds
    }
}