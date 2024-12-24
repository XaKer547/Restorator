using Restorator.Desktop.ViewModels;
using Wpf.Ui.Controls;

namespace Restorator.Desktop.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ReservationPage.xaml
    /// </summary>
    public partial class ReservationContentDialog : ContentDialog
    {
        public ReservationContentDialog(RestaurantReservationViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
