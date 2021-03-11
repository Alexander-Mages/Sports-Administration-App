using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required]
        public string TeamCode { get; set; }
        [Required]
        public bool TwoFactorEnabled { get; set; }
        public bool TotpEnabled { get; set; }
        public string QrCodeUrl { get; set; }
        public string TotpSetupCode { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
    }
}
