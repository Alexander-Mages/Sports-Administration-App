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

        public int TeamId { get; set; }
        public bool Coach { get; set; }
        public Team Team { get; set; }
        [Required]
        public string TeamCode { get; set; }
        //find n replace team to teamcode
        public bool TotpConfigured { get; set; }
        public string Code { get; set; }
        public bool TotpEnabled { get; set; }
        public string randomKey { get; set; }

        public string TotpSetupCode { get; set; }
        public string QrCodeUrl { get; set; }
        public PersonalRecord PersonalRecord { get; set; }
        public int PersonalRecordId { get; set; }
        
    }
}
