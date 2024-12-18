namespace Restorator.Domain.Models
{
    public class SessionInfo
    {
        public SessionInfo(int userId, string username, string role)
        {
            UserId = userId;
            Username = username;
            Role = role;
        }

        public int UserId { get; }
        public string Username { get; }
        public string Role { get; }
    }
}