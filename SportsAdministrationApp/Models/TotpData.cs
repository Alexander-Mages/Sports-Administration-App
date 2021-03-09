using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public class TotpData
    {
        public string TotpSetupCode { get; set; }
        public string QrCodeUrl { get; set; }
    }
}
