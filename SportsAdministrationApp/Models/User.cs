using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required]
        public string Team { get; set; }

        public string Code { get; set; }

         
        public PersonalRecord PersonalRecord { get; set; }
        public int PersonalRecordId { get; set; }
        
    }
}
