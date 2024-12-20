namespace Restorator.DataAccess.Data.Entities
{
    /// <summary>
    /// Шаблон стола
    /// </summary>
    public class TableTemplate
    {
        public int Id { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public int Rotation { get; set; }
    }
}