namespace Restorator.DataAccess.Data.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
