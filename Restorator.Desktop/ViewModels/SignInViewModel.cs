using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Services;
using Wpf.Ui;
using Wpf.Ui.Extensions;

namespace Restorator.Desktop.ViewModels
{
    public partial class SignInViewModel : AuthenticationViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISessionManager _sessionManager;
        private readonly ISnackbarService _snackbarService;


        public SignInViewModel(IAuthenticationService authenticationService,
                               ISessionManager sessionManager,
                               ISnackbarService snackbarService)
        {
            _authenticationService = authenticationService;
            _sessionManager = sessionManager;
            _snackbarService = snackbarService;
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
                _snackbarService.Show("Ой-ой", "Кажется такого пользователя нет", Wpf.Ui.Controls.ControlAppearance.Danger);

                return;
            }

            _sessionManager.SetSession(result.SessionInfo!);

            Authenticated = true;

            _snackbarService.Show("С возвращением", "Мы рады видеть тебя снова", Wpf.Ui.Controls.ControlAppearance.Success);
        }
    }
}
