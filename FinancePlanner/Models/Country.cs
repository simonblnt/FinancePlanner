using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class Country
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public string ContinentName { get; set; }
    }
}