using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Services
{
    public interface IEmailService : IEmailSender
    {
        public void SendAuthEmail(string email, string link);
        public void SendTwoFactorCode(string email, string code);
        public void SendPasswordResetLink(string email, string code);
        public void SendCoachInvite(string email, string code);
    }
}
