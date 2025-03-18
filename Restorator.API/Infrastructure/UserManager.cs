using Restorator.Domain.Services;

namespace Restorator.API.Infrastructure
{
    public class UserManager : IUserManager
    {
        private readonly HttpContext _context;
        public UserManager(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor.HttpContext!;
        }

        public bool TryGetUserId(out int userId)
        {
            userId = default;

            var idClaim = _context.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (idClaim is null)
                return false;

            if (!int.TryParse(idClaim, out userId))
                return false;

            return true;
        }
    }
}
