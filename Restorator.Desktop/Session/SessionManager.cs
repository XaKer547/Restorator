using Restorator.Domain.Models;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text.Json;

namespace Restorator.Desktop.Session
{
    public interface ISessionManager
    {
        void SetSession(SessionInfo sessionInfo);
        SessionInfo GetSessionInfo();
        void RemoveSession();
    }
    public class SessionManager : ISessionManager
    {
        private readonly IsolatedStorageFile _userStore = IsolatedStorageFile.GetUserStoreForAssembly();

        public SessionInfo GetSessionInfo()
        {
            using var stream = _userStore.OpenFile("session.json", FileMode.Open);

            return JsonSerializer.Deserialize<SessionInfo>(stream)!;
        }

        public void RemoveSession()
        {
            _userStore.Close();

            _userStore.Remove();
        }

        public void SetSession(SessionInfo sessionInfo)
        {
            using var stream = _userStore.OpenFile("session.json", FileMode.Create);

            var json = JsonSerializer.Serialize(sessionInfo);

            using var writer = new StreamWriter(stream);

            writer.Write(json);
        }
    }
}
