using System.ComponentModel.DataAnnotations.Schema;

namespace Restorator.DataAccess.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Account Account { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}