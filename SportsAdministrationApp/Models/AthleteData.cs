using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public class AthleteData
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int FromPersonalRecordId { get; set; }
        public decimal Time { get; set; }
        public PersonalRecord PersonalRecord { get; set; }
        public int PersonalRecordId { get; set; }
    }
}
