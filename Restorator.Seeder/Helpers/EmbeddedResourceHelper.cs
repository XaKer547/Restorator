using System.Reflection;

namespace Restorator.Seeder.Helpers
{
    public static class EmbeddedResourceHelper
    {
        private static Assembly _assembly;
        static EmbeddedResourceHelper()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public static byte[] GetByteArrayFromResource(string filename)
        {
            using Stream stream = _assembly.GetManifestResourceStream($"Restorator.Seeder.Resources.{filename}");

            using var memoryStream = new MemoryStream();

            stream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
