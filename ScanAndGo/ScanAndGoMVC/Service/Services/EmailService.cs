using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Security;
using ServiceLibrary.Services.Interfaces;
using ModelsLibrary.DtoModels;

namespace ServiceLibrary.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(Email emailModel, SecureSocketOptions secureSocketOptions = SecureSocketOptions.Auto)
        {
            var emailMessage = new MimeMessage();
            var from = _configuration["EmailSettings:From"];
            emailMessage.From.Add(new MailboxAddress("ScanAndGo", from));
            emailMessage.To.Add(new MailboxAddress(emailModel.To, emailModel.To));
            emailMessage.Subject = emailModel.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(emailModel.Content)
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.CheckCertificateRevocation = false;
                    client.Connect(_configuration["EmailSettings:SmtpServer"], 465, true);
                    client.Authenticate(_configuration["EmailSettings:From"], _configuration["EmailSettings:Password"]);
                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
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
