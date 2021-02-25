using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Services
{
    public class EmailService : IEmailService
    {

        //Email:From
        //Email:Host
        //Email:Port
        //Email:Password
        //Email:Username

        private IConfiguration _configuration;
        private SmtpClient _client;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            _client.Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]);
            _client.EnableSsl = true;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (MailMessage mailMessage = new MailMessage(_configuration["Email:From"],
                email,
                subject,
                htmlMessage))
            {
                try
                {
                    _client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: {0}", ex.ToString());
                }
            }
            return null;
        }

        public void SendAuthEmail(string email, string link)
        {
            SendEmailAsync(email, "Sports Administration App,", $"click here to confirm your email: {link}");
        }
    }
}
