using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ProjectManager.Services
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            //Dane logowania
            //var pwd = ConfigurationSettings.AppSettings["MAIL_PASSWORD"];
           // var userName = ConfigurationSettings.AppSettings["MAIL"];
            var pwd = ConfigurationManager.AppSettings["MAIL_PASSWORD"];
            var userName = ConfigurationManager.AppSettings["MAIL"];
            var sentFrom = userName;

            //Konfiguracja poczty
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp-mail.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Creatte the credentials:
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(userName, pwd);
            client.EnableSsl = true;
            client.Credentials = credentials;
            
            // Create the message:

            var mail = new System.Net.Mail.MailMessage(sentFrom, message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            
            // Send:
            return client.SendMailAsync(mail);
            
        }
    }
}
