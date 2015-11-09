using System.Configuration;
using System.Net;
using System.Net.Mail;
using ProjectManager.Services.Interfaces;

namespace ProjectManager.Services
{
    public class EmailService : IEmailService
    {
        private string emailAddress { get; }
        private string password { get; }
        private SmtpClient smtpClient { get; }


        public EmailService()
        {
            emailAddress = ConfigurationManager.AppSettings[nameof(emailAddress)];
            password = ConfigurationManager.AppSettings[nameof(password)];

            smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(emailAddress, password)
            };
        }

        public void SendEmail(string sendTo, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage(emailAddress, sendTo)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            };

            smtpClient.Send(mailMessage);
        }
    }
}
