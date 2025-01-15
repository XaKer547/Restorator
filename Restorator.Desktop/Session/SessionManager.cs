using Restorator.Domain.Models;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text.Json;

namespace Restorator.Desktop.Session
{
    public interface ISessionManager
    {
        void SetSession(SessionInfo sessionInfo);
        SessionInfo? GetSessionInfo();
        void RemoveSession();

        bool HaveSession();
    }
    public class SessionManager : ISessionManager
    {
        private readonly IsolatedStorageFile _userStore = IsolatedStorageFile.GetUserStoreForAssembly();

        public SessionInfo? GetSessionInfo()
        {
            using var stream = _userStore.OpenFile("session.json", FileMode.Open);

            return JsonSerializer.Deserialize<SessionInfo>(stream);
        }

        public bool HaveSession()
        {
            if (!_userStore.FileExists("session.json"))
            {
                using var stream = _userStore.CreateFile("session.json");

                using var writer = new StreamWriter(stream);

                writer.Write("{\n}");

                return false;
            }

            var info = GetSessionInfo();

            if (info is null)
                return false;

            return info.Role is not null;
        }

        public void RemoveSession()
        {
            using var stream = _userStore.OpenFile("session.json", FileMode.Open);

            using var writer = new StreamWriter(stream);

            writer.BaseStream.SetLength(0);

            writer.Write("{\n}");
        }

        public void SetSession(SessionInfo sessionInfo)
        {
            using var stream = _userStore.OpenFile("session.json", FileMode.OpenOrCreate);

            var json = JsonSerializer.Serialize(sessionInfo);

            using var writer = new StreamWriter(stream);

            writer.BaseStream.SetLength(0);

            writer.Write(json);
        }
    }
}
