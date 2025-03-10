using System.Security.Claims;

namespace Restorator.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool TryGetUserId(this ClaimsPrincipal user, out int userId)
        {
            userId = default;

            var idClaim = user.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (idClaim is null)
                return false;

            if (!int.TryParse(idClaim, out userId))
                return false;

            return true;
        }
    }
}
