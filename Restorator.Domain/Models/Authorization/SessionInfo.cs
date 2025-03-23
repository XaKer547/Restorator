namespace Restorator.Domain.Models.Authorization
{
    public class SessionInfo
    {
        public SessionInfo(string username, string role)
        {
            Username = username;
            Role = role;
        }

        public string Username { get; }
        public string Role { get; }
    }
}