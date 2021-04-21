using System;

namespace FinancePlanner.Models.UserSpecific
{
    public class Stat
    {
        private int Id { get; set; }
        private int UserId { get; set; }
        private int CategoryId { get; set; }
        private int ValueTypeId { get; set; }
    }
}