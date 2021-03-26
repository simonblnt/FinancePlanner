using System;

namespace FinancePlanner.Models.Main
{
    public class User
    {
        private int Id { get; set; }
        private string UserName { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Password { get; set; }
        private int PaidLeavesTotal { get; set; }
        private int PaidLeavesUser { get; set; }
        private string Email { get; set; }
        private DateTime RegisteredAt { get; set; } 
    }
}