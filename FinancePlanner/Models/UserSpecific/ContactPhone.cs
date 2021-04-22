using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models.UserSpecific
{
    public class ContactPhone
    {
        [Key] public int Id { get; set; }
        public int UserId { get; set; }
        public int ContactId { get; set; }
        public string Number { get; set; }
    }
}