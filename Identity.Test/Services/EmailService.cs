
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Identity.Test.Services
{
    public class EmailService
    {
        // Config your mail to use:  https://myaccount.google.com/lesssecureapps
        // Enter your mail and password on the code
      
        public Task Execute(string UserMail,string body,string subject)
        {
            SmtpClient Client = new SmtpClient();
            Client.Port = 587;
            Client.Host = "smtp.gmail.com";
            Client.EnableSsl = true;
            Client.Timeout = 1000000;
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            Client.UseDefaultCredentials = false;
            Client.Credentials = new NetworkCredential("mail","password");
            MailMessage message = new MailMessage("mail",UserMail,subject,body);
            message.IsBodyHtml = true;
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            Client.Send(message);
            return Task.CompletedTask;    
        }
    }
}
