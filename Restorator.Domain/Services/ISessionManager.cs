using Restorator.Domain.Models.Authorization;

namespace Restorator.Domain.Services
{
    public delegate void UserLoggedInHandler();
    public interface ISessionManager
    {
        bool TryGetSession(out SessionInfo sessionInfo);
        bool TryGetToken(out string token);
        void SetSession(SessionInfo sessionInfo, string token);
        bool HaveSession();
        void RemoveSession();
        
        public event UserLoggedInHandler? UserLoggedIn;
    }
}