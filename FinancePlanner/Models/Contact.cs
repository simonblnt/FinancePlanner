using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class Contact
    {
        [Key] public int Id { get; set; }
        public int CountryId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Role { get; set; }
    }
}