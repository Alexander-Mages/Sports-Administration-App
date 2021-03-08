using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.ViewModels
{
    public class TotpConfirmViewModel
    {
        public int Code { get; set; }
        public string QrCodeUrl { get; set; }
        public string TotpSetupCode { get; set; }
    }
}
