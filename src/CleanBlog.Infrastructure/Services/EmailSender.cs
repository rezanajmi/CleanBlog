using CleanBlog.Domain.Abstractions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CleanBlog.Infrastructure.Services
{
    internal class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;
        private readonly ILogger<EmailSender> logger;

        public EmailSender(ILogger<EmailSender> logger,
            IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
            this.logger = logger;
        }

        public async Task<bool> SendEmail(string to, string subject, string content, CancellationToken ct)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", emailSettings.From));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = content };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Timeout = 500;
                    await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.Port, true, ct);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(emailSettings.UserName, emailSettings.Password, ct);
                    await client.SendAsync(emailMessage, ct);
                }
                catch (Exception exc)
                {
                    logger.LogError(exc, exc.Message);
                }
            }

            return true;
        }
    }
}
