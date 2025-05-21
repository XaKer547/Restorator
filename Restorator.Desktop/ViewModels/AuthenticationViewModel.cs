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
        private readonly AccountRestoreViewModel _accountRestoreViewModel;
        public AuthenticationViewModel(Services.INavigationService navigationService,
                                       SignInViewModel signInViewModel,
                                       SignUpViewModel signUpViewModel,
                                       AccountRestoreViewModel accountRestoreViewModel)
        {
            _navigationService = navigationService;
            _signInViewModel = signInViewModel;
            _signUpViewModel = signUpViewModel;

            NavigateToSignInPage();
            _accountRestoreViewModel = accountRestoreViewModel;
        }

        [ObservableProperty]
        private AuthenticationViewModelBase currentViewModel;

        [ObservableProperty]
        private IRelayCommand navigateBackCommand;

        [RelayCommand]
        public void NavigateToSignUpPage()
        {
            CurrentViewModel = _signUpViewModel;
            NavigateBackCommand = NavigateToSignInPageCommand;
        }

        [RelayCommand]
        public void NavigateToSignInPage()
        {
            CurrentViewModel = _signInViewModel;
            NavigateBackCommand = NavigateToMenuCommand;
        }

        [RelayCommand]
        public void NavigateToPasswordRestorePage()
        {
            CurrentViewModel = _accountRestoreViewModel;
            NavigateBackCommand = NavigateToSignInPageCommand;
        }

        [RelayCommand]
        public async Task NavigateToMenu()
        {
            if (CurrentViewModel.Authenticated && CurrentViewModel.Role == Domain.Models.Enums.Roles.User)
                await _navigationService.NavigateBackAsync();
            else
                await _navigationService.NavigateAsync<MenuViewModel>();
        }
    }
}