using Restorator.Mail.Models.Templates.Abstract;

namespace Restorator.API.Infrastructure
{
    public class MailTemplateBuilder
    {
        private readonly IWebHostEnvironment _environment;
        public MailTemplateBuilder(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string BuildMailBodyFromTemplate(MailTemplateBase mailTemplate)
        {
            var path = Path.Combine(_environment.WebRootPath, "MailTemplates", $"{mailTemplate.TemplateName}.html");

            using var reader = new StreamReader(path);

            var body = reader.ReadToEnd();

            foreach (var pair in mailTemplate.ToDictionary())
                body = body.Replace(pair.Key, pair.Value);

            return body;
        }
    }
}
