using System.Security.Cryptography;
using System.Text;

namespace Restorator.DataAccess.Helpers
{
    public static class AccountPasswordHelper
    {
        public static string HashUserPassword(string password)
        {
            var bytes = Encoding.Unicode.GetBytes(password);

            var hash = SHA256.HashData(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}