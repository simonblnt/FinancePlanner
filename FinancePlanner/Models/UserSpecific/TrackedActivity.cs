using System;
using Microsoft.AspNetCore.Antiforgery;
using Newtonsoft.Json.Serialization;

namespace FinancePlanner.Models.UserSpecific
{
    public class TrackedActivity
    {
        private int Id { get; set; }
        private int UserId { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }
        private int CategoryId { get; set; }
        private string Location { get; set; }
    }
}