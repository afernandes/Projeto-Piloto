using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Semp.Module.Core.Services;

namespace Semp.Module.EmailSenderSmtp
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfig _emailConfig = new EmailConfig();

        public EmailSender(IConfiguration config)
        {
            _emailConfig.SmtpServer = config.GetValue<string>("SmtpServer");
            _emailConfig.SmtpUsername = config.GetValue<string>("SmtpUsername");
            _emailConfig.SmtpPassword = config.GetValue<string>("SmtpPassword");
            _emailConfig.SmtpPort = config.GetValue<int>("SmtpPort");
        }
        
        public async Task SendEmailAsync(string email, string subject, string body, bool isHtml = false)
        {
            MailMessage mailMessage = new MailMessage();

            string str = isHtml ? "text/html; " : "text/plain; ";

            str = string.Concat(str, "charset=", "utf-8");
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(body, new System.Net.Mime.ContentType(str));
            mailMessage.AlternateViews.Add(alternateView);
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.Subject = subject;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = isHtml;

            mailMessage.From = new MailAddress("painelintegracao@semptcl.com.br");
            //mailMessage.Sender = new MailAddress("painelintegracao@semptcl.com.br");

            mailMessage.To.Add(new MailAddress(email));

            SmtpClient smtpClient = new System.Net.Mail.SmtpClient(_emailConfig.SmtpServer/*, _emailConfig.SmtpPort*/)
            {
                UseDefaultCredentials = false
            };

            smtpClient.Credentials = new NetworkCredential(_emailConfig.SmtpUsername, _emailConfig.SmtpPassword, "ST");

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
