using System.Windows.Controls;
using Restorator.Desktop.ViewModels;
using Wpf.Ui.Abstractions.Controls;

namespace Restorator.Desktop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserReservationsPage.xaml
    /// </summary>
    public partial class UserReservationsPage : Page, INavigableView<UserReservationsViewModel>
    {
        public UserReservationsPage(UserReservationsViewModel viewModel)
        {
            ViewModel = viewModel;

            DataContext = ViewModel;

            InitializeComponent();
        }

        public UserReservationsViewModel ViewModel { get; }
    }
}
