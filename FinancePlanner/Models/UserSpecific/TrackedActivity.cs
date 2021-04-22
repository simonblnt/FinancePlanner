using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Antiforgery;
using Newtonsoft.Json.Serialization;

namespace FinancePlanner.Models.UserSpecific
{
    public class TrackedActivity
    {
        [Key] public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; }
        public string Location { get; set; }
    }
}