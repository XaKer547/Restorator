using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface ISessionManager
    {
        void SetSession(SessionInfo sessionInfo);
        SessionInfo? GetSessionInfo();
        void RemoveSession();
        bool HaveSession();
    }
}
