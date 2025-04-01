using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.ViewModels.Abstract;

namespace Restorator.Desktop.ViewModels
{
    public partial class AuthenticationViewModel : ViewModelBase
    {
        private readonly Services.INavigationService _navigationService;
        private readonly SignInViewModel _signInViewModel;
        private readonly SignUpViewModel _signUpViewModel;
        public AuthenticationViewModel(Services.INavigationService navigationService,
                                       SignInViewModel signInViewModel,
                                       SignUpViewModel signUpViewModel)
        {
            _navigationService = navigationService;
            _signInViewModel = signInViewModel;
            _signUpViewModel = signUpViewModel;

            DefaultReturnAction = NavigateToUserMenuCommand;

            NavigateToSignInPage();
        }

        [ObservableProperty]
        private AuthenticationViewModelBase currentViewModel;

        [ObservableProperty]
        private IRelayCommand _defaultReturnAction;

        [RelayCommand]
        public void NavigateToSignUpPage()
        {
            CurrentViewModel = _signUpViewModel;
        }

        [RelayCommand]
        public void NavigateToSignInPage()
        {
            CurrentViewModel = _signInViewModel;
        }

        [RelayCommand]
        public async Task NavigateToUserMenu()
        {
            await _navigationService.NavigateAsync<MenuViewModel>();
        }

        [RelayCommand]
        public async Task NavigateBack()
        {
            await _navigationService.NavigateBackAsync();
        }

        public Task SetStackNavigation()
        {
            DefaultReturnAction = NavigateBackCommand;

            return Task.CompletedTask;
        }
    }
}