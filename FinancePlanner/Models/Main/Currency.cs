using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models.Main
{
    public class Currency
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
    }
}