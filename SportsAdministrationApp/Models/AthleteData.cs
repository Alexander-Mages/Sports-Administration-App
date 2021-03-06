using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public class AthleteData
    {
        public int Id { get; set;}
        public int Score { get; set; }
        public List<Times> Times { get; set; }
    }
}
