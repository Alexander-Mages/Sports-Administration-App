using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public class Times
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FromAthleteDataId { get; set; }
        public int PersonalRecord { get; set; }

        public AthleteData AthleteData { get; set; }
        public int AthleteDataId { get; set; }
    }
}
