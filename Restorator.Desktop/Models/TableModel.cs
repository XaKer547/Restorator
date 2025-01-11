using CommunityToolkit.Mvvm.ComponentModel;
using Restorator.Domain.Models.Enums;

namespace Restorator.Desktop.Models
{
    public partial class TableModel : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private TableStates state;

        [ObservableProperty]
        private double x;

        [ObservableProperty]
        private double y;

        [ObservableProperty]
        private double rotation;

        [ObservableProperty]
        private double height;

        [ObservableProperty]
        private double width;
    }
}
