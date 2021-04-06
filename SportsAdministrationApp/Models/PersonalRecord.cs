using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public class PersonalRecord
    {
        public int Id { get; set; }
        public decimal PR { get; set; }
        public List<AthleteData> AthleteData { get; set; }

        public PersonalRecord()
        {
            AthleteData = new List<AthleteData>();
        }
    }
}
