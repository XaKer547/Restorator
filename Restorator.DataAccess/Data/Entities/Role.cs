using Restorator.DataAccess.Data.Entities.Enums;
using Restorator.DataAccess.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restorator.DataAccess.Data.Entities
{
    public class Role
    {
        private Role(Roles @enum)
        {
            Id = (int)@enum;
            Name = @enum.ToString();
            Description = @enum.GetEnumDescription();
        }
        protected Role() { } //For EF

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public static implicit operator Role(Roles @enum) => new(@enum);
        public static implicit operator Roles(Role faculty) => (Roles)faculty.Id;

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
