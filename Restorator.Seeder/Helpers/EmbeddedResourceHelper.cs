using System.Reflection;

namespace Restorator.Seeder.Helpers
{
    public static class EmbeddedResourceHelper
    {
        private static readonly Assembly _assembly;
        static EmbeddedResourceHelper()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public static byte[] GetByteArrayFromResource(string filename)
        {
            using Stream stream = _assembly.GetManifestResourceStream($"Restorator.Seeder.Resources.{filename}.png");

            using var memoryStream = new MemoryStream();

            stream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }

        public static byte[] GetRestaurantImage(string filename)
        {
            return GetByteArrayFromResource($"RestaurantsImage.{filename}");
        }

        public static byte[] GetRestaurantPlan(string filename)
        {
            return GetByteArrayFromResource($"RestaurantsPlan.{filename}");
        }

        public static byte[] GetRestaurantMenu(string filename)
        {
            return GetByteArrayFromResource($"RestaurantsMenu.{filename}");
        }
    }
}
