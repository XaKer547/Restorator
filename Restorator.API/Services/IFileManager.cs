namespace Restorator.API.Services
{
    public class RestaurantImagesManager
    {
        private readonly IWebHostEnvironment _enviroment;
        public RestaurantImagesManager(IWebHostEnvironment enviroment)
        {
            _enviroment = enviroment;
        }

        public async Task<IEnumerable<string>> UpdateRestaurantAsync(string restaurantName, byte[] menu, IEnumerable<byte[]> images)
        {
            DeleteRestaurant(restaurantName);

            var names = await CreateRestaurant(restaurantName, menu, images);

            return names;
        }
        public async Task<IEnumerable<string>> CreateRestaurant(string restaurantName, byte[] menu, IEnumerable<byte[]> images)
        {
            var path = GetPath(restaurantName);

            Directory.CreateDirectory(path);

            await UploadMenuAsync(path, menu);

            var names = await UploadImagesAsync(path, images);

            return names;
        }

        public async Task UploadMenuAsync(string dirPath, byte[] menu)
        {
            var path = Path.Combine(dirPath, "menu.png");

            await File.WriteAllBytesAsync(path, menu);
        }
        public async Task<IEnumerable<string>> UploadImagesAsync(string dirPath, IEnumerable<byte[]> images)
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
        public void DeleteRestaurant(string restaurantName)
        {
            Directory.Delete(GetPath(restaurantName), true);
        }

        private string GetPath(string restaurantName) => Path.Combine(_enviroment.WebRootPath, "Restaurants", restaurantName);
    }
}
