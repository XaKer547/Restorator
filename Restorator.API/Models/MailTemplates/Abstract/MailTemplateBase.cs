namespace Restorator.API.Models.MailTemplates.Abstract
{
    public abstract class MailTemplateBase
    {
        public abstract string TemplateName { get; }
        public abstract string SubjectName { get; }
        public abstract Dictionary<string, string> ToDictionary();
    }
}
