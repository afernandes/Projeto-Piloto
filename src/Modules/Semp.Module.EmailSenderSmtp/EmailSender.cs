using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
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
        
        /*
        public async Task SendEmailAsync(string email, string subject, string body, bool isHtml = false)
        {
            var message = new MimeMessage();

            string mailFrom = "semptcl@semptcl.com.br";

            message.From.Add(new MailboxAddress(mailFrom));
            message.To.Add(new MailboxAddress(email));
            message.Subject = subject;

            var textFormat = isHtml ? TextFormat.Html : TextFormat.Plain;
            message.Body = new TextPart(textFormat)
            {
                Text = body
            };

            using (var client = new System.Net.Mail.SmtpClient(new ProtocolLogger("smtp.log")))
            {
                // Accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.SmtpPort, SecureSocketOptions.Auto);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                //await client.AuthenticateAsync(_emailConfig.SmtpUsername, _emailConfig.SmtpPassword);
                var netwokCredential = new System.Net.NetworkCredential(_emailConfig.SmtpUsername, _emailConfig.SmtpPassword, "ST");
                var ntlmCredential = new SaslMechanismNtlm(netwokCredential);

                await client.AuthenticateAsync(ntlmCredential);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        */

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
