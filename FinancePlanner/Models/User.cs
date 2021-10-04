using System;
using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models
{
    public class User
    {
        [Key] public int Id { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAt { get; set; }
        
        public int CountryId { get; set; }

        public string Gender { get; set; }
        
        public string Height { get; set; }
        
        public string Weight { get; set; }
        
        public DateTime Dob { get; set; }

        public int PaidLeavesTotal { get; set; }
        public int PaidLeavesUser { get; set; }
        
    }
}