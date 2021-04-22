using System;
using System.ComponentModel.DataAnnotations;

namespace FinancePlanner.Models.UserSpecific
{
    public class Stat
    {
        [Key] public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int ValueTypeId { get; set; }
    }
}