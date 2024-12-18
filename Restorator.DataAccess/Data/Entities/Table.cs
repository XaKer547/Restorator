using System.ComponentModel.DataAnnotations.Schema;

namespace Restorator.DataAccess.Data.Entities
{
    public class Table
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Template))]
        public int TableTemplateId { get; set; }
        public TableTemplate Template { get; set; }

        //position on map
        public float X { get; set; }
        public float Y { get; set; }
    }
}