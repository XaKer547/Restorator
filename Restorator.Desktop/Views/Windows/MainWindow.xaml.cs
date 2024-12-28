using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Restorator.Desktop.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        public MainWindow(MainWindowViewModel viewModel,
            Services.INavigationService navigationService,
            IContentDialogService contentDialogService,
            ISnackbarService snackbarService,
            ISessionManager sessionManager)
        {
            SystemThemeWatcher.Watch(this);

            DataContext = viewModel;

            InitializeComponent();

            navigationService.SetNavigationControl(RootNavigation);
            contentDialogService.SetDialogHost(RootContentDialog);
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);

            if (sessionManager.HaveSession())
            {
                navigationService.Navigate<MenuViewModel>();
                return;
            }

            navigationService.Navigate<AuthenticationViewModel>();
        }
    }
}