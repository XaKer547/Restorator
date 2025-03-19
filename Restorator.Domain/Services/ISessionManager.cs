using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface ISessionManager
    {
        bool TryGetSession(out SessionInfo sessionInfo);
        void SetSession(SessionInfo sessionInfo, string token);
        void RemoveSession();
    }
}
