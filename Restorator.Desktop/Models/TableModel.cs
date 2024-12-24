using CommunityToolkit.Mvvm.ComponentModel;
using Restorator.Domain.Models.Enums;

namespace Restorator.Desktop.Models
{
    public partial class TableModel : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private TableStates state;
        public float X { get; set; }
        public float Y { get; set; }

        public float Height { get; set; }
        public float Width { get; set; }
        public double Rotation { get; set; } = 0;
    }
}
