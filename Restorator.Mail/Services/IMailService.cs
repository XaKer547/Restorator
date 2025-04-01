using Restorator.Mail.Models.Templates.Abstract;

namespace Restorator.Mail.Services
{
    public interface IMailService
    {
        Task SendMailAsync(MailTemplateBase mailTemplate);
    }
}