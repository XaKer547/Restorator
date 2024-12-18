using CommunityToolkit.Mvvm.ComponentModel;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models.Enums;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantInteractiveTableViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string image;

        public TableStates TableState
        {
            get => tableState;

            set
            {
                if (tableState == value)
                    return;

                tableState = value;

                OnPropertyChanged(nameof(Occupied));
            }
        }
        private TableStates tableState;

        public bool Interactive => TableState != TableStates.OccupiedByOther;
        public bool Occupied => TableState == TableStates.OccupiedByUser;
    }
}

//ListBox

//Canvas.X Canvas.Y

//state