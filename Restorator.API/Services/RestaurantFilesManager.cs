using Restorator.Application.Server.Models;
using Restorator.Application.Server.Services.Abstract;

namespace Restorator.API.Services
{
    public class RestaurantFilesManager : IRestaurantFilesManager
    {
        private readonly IWebHostEnvironment _enviroment;
        public RestaurantFilesManager(IWebHostEnvironment enviroment)
        {
            _enviroment = enviroment;
        }

        public async Task<RestaurantFilesInfoDTO> CreateRestaurantFolder(string restaurantName, IEnumerable<byte[]> images, byte[] menu)
        {
            var path = GetPath(restaurantName);

            Directory.CreateDirectory(path);

            var menuPath = await UploadMenuAsync(path, menu);

            var imagesPath = await UploadImagesAsync(path, images);

            return new RestaurantFilesInfoDTO
            {
                ImagesPath = imagesPath,
                MenuPath = menuPath,
            };
        }

        public async Task<RestaurantFilesInfoDTO> UpdateRestaurantFolder(string restaurantName, IEnumerable<byte[]> images, byte[] menu)
        {
            var path = GetPath(restaurantName);

            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }

            var menuPath = await UploadMenuAsync(path, menu);

            var imagesPath = await UploadImagesAsync(path, images);

            return new RestaurantFilesInfoDTO
            {
                ImagesPath = imagesPath,
                MenuPath = menuPath,
            };
        }

        public Task DeleteRestaurantFolder(string restaurantName)
        {
            Directory.Delete(GetPath(restaurantName), true);

            return Task.CompletedTask;
        }

        private async Task<string> UploadMenuAsync(string dirPath, byte[] menu)
        {
            if (menu is null)
                return string.Empty;

            var fileName = "menu.png";

            var path = Path.Combine(dirPath, fileName);

            await File.WriteAllBytesAsync(path, menu);

            return fileName;
        }
        private async Task<IEnumerable<string>> UploadImagesAsync(string dirPath, IEnumerable<byte[]> images)
        {
            var names = new List<string>();

            foreach (var image in images)
            {
                var name = $"{Guid.NewGuid()}.png";

                var path = Path.Combine(dirPath, name);

                await File.WriteAllBytesAsync(path, image);

                names.Add(name);
            }

            return names;
        }
        private string GetPath(string restaurantName) => Path.Combine(_enviroment.WebRootPath, "Restaurants", restaurantName);
    }
}