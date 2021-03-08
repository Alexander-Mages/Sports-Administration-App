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

            //appsecrets debug:
            //Console.WriteLine(_configuration["Email:Password"]);
            //Console.WriteLine(_configuration["Email:From"]);
            //Console.WriteLine(_configuration["Email:Host"]);
            //Console.WriteLine(_configuration["Email:Port"]);
            //Console.WriteLine(_configuration["Email:Username"]);

            _client = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            _client.UseDefaultCredentials = false;
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
            SendEmailAsync(email, "Sports Administration App: Please confirm email", $"click here to confirm your email: {link}");
        }
        public void SendTwoFactorCode(string email, string code)
        {
            SendEmailAsync(email, "Sports Administration App: Your 2FA code", $"Your two factor authentication code is: {code}\nplease input this code to login");
        }
        public void SendPasswordResetLink(string email, string link)
        {
            SendEmailAsync(email, "Sports Administration App: Password Reset", $"click here to reset your password: {link}\nIf you have not requested a password change, you can safely discard this email");
        }
    }
}
