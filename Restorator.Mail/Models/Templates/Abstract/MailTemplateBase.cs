namespace Restorator.Mail.Models.Templates.Abstract
{
    public abstract class MailTemplateBase
    {
        public string TemplateName { get; }
        public string SubjectName { get; }
        public string Email { get; }

        public MailTemplateBase(string templateName,
                                string subjectName,
                                string email)
        {
            TemplateName = templateName;
            SubjectName = subjectName;
            Email = email;
        }

        public abstract Dictionary<string, string> ToDictionary();
    }
}
