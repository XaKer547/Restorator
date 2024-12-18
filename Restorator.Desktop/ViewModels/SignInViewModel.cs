using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Services;

namespace Restorator.Desktop.ViewModels
{
    public partial class SignInViewModel : AuthenticationViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        public SignInViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string password;

        [RelayCommand]
        public async Task SignIn()
        {
            var signInDto = new SignInDTO()
            {
                Login = Login,
                Password = Password
            };

            var result = await _authenticationService.SignInAsync(signInDto);

            if (!result.Success)
            {
                return;
            }
        }
    }
}
