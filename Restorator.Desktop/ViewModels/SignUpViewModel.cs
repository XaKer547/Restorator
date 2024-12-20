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
    public partial class SignUpViewModel : AuthenticationViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISessionManager _sessionManager;
        private readonly ISnackbarService _snackbarService;
        public SignUpViewModel(IAuthenticationService authenticationService,
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
        public async Task SignUp()
        {
            var signUnDto = new SignUpDTO()
            {
                Login = Login,
                Password = Password
            };

            var result = await _authenticationService.SignUpAsync(signUnDto);

            if (!result)
            {
                _snackbarService.Show("Упс", "У нас не получилось тебя зарегистрировать, попробуй чуть позже", Wpf.Ui.Controls.ControlAppearance.Danger);

                return;
            }

            var session = await _authenticationService.SignInAsync(new SignInDTO()
            {
                Login = Login,
                Password = Password
            });

            _sessionManager.SetSession(session.SessionInfo!);

            Authenticated = true;

            _snackbarService.Show("Добро пожаловать в семью", "Let's celebrate and eat some chick", Wpf.Ui.Controls.ControlAppearance.Success);
        }
    }
}