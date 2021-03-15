using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public class Team
    {
        public List<User> User { get; set; }
        public string Name { get; set; }
        public string HeadCoach { get; set; }
        public string TeamCode { get; set; }
        // change to team code
        public int Id { get; set; }
        public string CoachCode { get; set; }
    }
}
