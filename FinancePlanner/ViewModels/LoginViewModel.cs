using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using Microsoft.AspNetCore.Routing.Constraints;

namespace FinancePlanner.ViewModels
{
    public class LoginViewModel
    {
        [Required] 
        [EmailAddress] 
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}