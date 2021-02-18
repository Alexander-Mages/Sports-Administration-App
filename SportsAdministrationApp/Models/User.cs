using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public class User : IdentityUser<int>
    {
        //public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set;}
        ///[Required]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        //public string Email { get; set; }
        [Required]
        public string Team { get; set; }
    }
}
