using Restorator.Desktop.Properties;
using Restorator.Domain.Models.Authorization;
using Restorator.Domain.Services;

namespace Restorator.Desktop.Session
{
    public class SessionManager : ISessionManager
    {
        private readonly Settings _settings = Settings.Default;

        public event UserLoggedInHandler? UserLoggedIn;
        public bool TryGetSession(out SessionInfo sessionInfo)
        {
            sessionInfo = null;

            if (!HaveSession())
                return false;

            sessionInfo = new(_settings.Username, _settings.Role);

            return true;
        }
        public void RemoveSession()
        {
            _settings.Reset();

            _settings.Save();
        }
        public void SetSession(SessionInfo sessionInfo, string token)
        {
            _settings.Token = token;

            _settings.Role = sessionInfo.Role;

            _settings.Username = sessionInfo.Username;

            _settings.Save();

            UserLoggedIn?.Invoke();
        }
        public bool HaveSession() => _settings.Token != string.Empty;
        public bool TryGetToken(out string token)
        {
            if (HaveSession())
            {
                token = _settings.Token;

                return true;
            }

            token = null;

            return false;
        }

    }
}