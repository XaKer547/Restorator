using Restorator.Application.Server.Services.Abstract;

namespace Restorator.API.Services
{
    public class RestaurantTemplateFilesManager : IRestaurantTemplateFilesManager
    {
        private readonly IWebHostEnvironment _enviroment;
        public RestaurantTemplateFilesManager(IWebHostEnvironment enviroment)
        {
            _enviroment = enviroment;
        }
        public Task DeleteTemplate(string templatePath)
        {
            File.Delete(GetSchemePath(templatePath));

            return Task.CompletedTask;
        }
        public async Task UpdateTemplate(string templatePath, byte[] template)
        {
            await File.WriteAllBytesAsync(GetSchemePath(templatePath), template);
        }
        public async Task<string> UploadTemplate(byte[] template)
        {
            var fileName = $"{Guid.NewGuid()}.png";

            await File.WriteAllBytesAsync(GetSchemePath(fileName), template);

            return fileName;
        }

        private string GetSchemePath(string templatePath) => Path.Combine(_enviroment.WebRootPath, "schemes", templatePath);
    }
}
