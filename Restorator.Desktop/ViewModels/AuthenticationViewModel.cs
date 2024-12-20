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

            NavigateToSignInPage();
        }

        [ObservableProperty]
        private AuthenticationViewModelBase currentViewModel;

        [ObservableProperty]
        private byte[] _imageTest;

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
        public void NavigateToUserMenu()
        {
            _navigationService.Navigate<RestaurantSearchViewModel>();
        }
    }
}