using CrmApiV2.Dtos.Email;
using CrmApiV2.Interface;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using CrmApiV2.Models;

namespace CrmApiV2.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void SendEmail(EmailDto request)
        {
            var email = CreateEmailMessage(request);
            Send(email);
        }

        private MimeMessage CreateEmailMessage(EmailDto request)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            emailMessage.To.Add(MailboxAddress.Parse(request.To));
            emailMessage.Subject = request.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = request.Body };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailSettings.EmailHost, _emailSettings.EmailPort, SecureSocketOptions.StartTls);
                    client.Authenticate(_emailSettings.EmailUsername, "zlyy ildh akvz ywkr");
                    client.Send(mailMessage);
                }
                catch
                {
                    // Handle the exception (logging, rethrowing, etc.)
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
