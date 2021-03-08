using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public class Team
    {
        public User User { get; set; }
        public string Name { get; set; }
        public string HeadCoach { get; set; }
        public string TeamCode { get; set; }

    }
}
