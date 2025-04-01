using System.Security.Cryptography;
using System.Text;

namespace Restorator.DataAccess.Helpers
{
    public static class AccountPasswordHelper
    {
        public static string GenereateOtpCode(int length = 6)
        {
            ReadOnlySpan<char> chars = "0123456789".ToCharArray();

            var code = RandomNumberGenerator.GetItems(chars, length);

            return new string(code);
        }

        public static string HashUserPassword(string password)
        {
            var bytes = Encoding.Unicode.GetBytes(password);

            var hash = SHA256.HashData(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}