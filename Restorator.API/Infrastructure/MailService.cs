using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Restorator.API.Configuration;
using Restorator.API.Models.MailTemplates.Abstract;

namespace Restorator.API.Infrastructure
{
    public class MailService
    {
        private readonly SmtpConfiguration _configuration;
        private readonly MailTemplateBuilder _mailTemplateBuilder;
        public MailService(SmtpConfiguration configuration,
                           MailTemplateBuilder mailTemplateBuilder)
        {
            _configuration = configuration;
            _mailTemplateBuilder = mailTemplateBuilder;
        }

        public async Task SendMailAsync(string email, MailTemplateBase mailTemplate)
        {
            using var message = new MimeMessage()
            {
                Subject = mailTemplate.SubjectName,
                Body = new BodyBuilder()
                {
                    HtmlBody = _mailTemplateBuilder.BuildMailBodyFromTemplate(mailTemplate)
                }.ToMessageBody()
            };

            message.From.Add(new MailboxAddress("Ресторатор", _configuration.Username));

            message.To.Add(new MailboxAddress("Вы", email));

            using var smtp = new SmtpClient()
            {
                CheckCertificateRevocation = false
            };

            await smtp.ConnectAsync(_configuration.SmtpServer, _configuration.Port, SecureSocketOptions.Auto);

            await smtp.AuthenticateAsync(_configuration.Username, _configuration.Password);

            await smtp.SendAsync(message);

            await smtp.DisconnectAsync(true);
        }
    }
}