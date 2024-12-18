using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restorator.DataAccess.Data.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Time)]
        public TimeOnly BeginWorkTime { get; set; }

        [DataType(DataType.Time)]
        public TimeOnly EndWorkTime { get; set; }

        [ForeignKey(nameof(Template))]
        public int TemplateId { get; set; }
        public RestaurantTemplate Template { get; set; }
    }
}