using System;
using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models.Main
{
    public class User
    {
        [Key] public int Id { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int PaidLeavesTotal { get; set; }
        public int PaidLeavesUser { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAt { get; set; } 
    }
}