using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class Wallet
    {
        [Key] public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}