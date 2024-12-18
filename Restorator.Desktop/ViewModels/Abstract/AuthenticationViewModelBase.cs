using CommunityToolkit.Mvvm.ComponentModel;

namespace Restorator.Desktop.ViewModels.Abstract
{
    public partial class AuthenticationViewModelBase : ViewModelBase
    {
        [ObservableProperty]
        public bool _authenticated;
    }
}