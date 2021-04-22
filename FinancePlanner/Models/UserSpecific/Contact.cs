using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models.UserSpecific
{
    public class Contact
    {
        [Key] public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
    }
}