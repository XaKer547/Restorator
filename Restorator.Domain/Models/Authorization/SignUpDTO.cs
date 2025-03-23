namespace Restorator.Domain.Models.Authorization
{
    public class SignUpDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
    }
}
