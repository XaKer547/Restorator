using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.DataAccess.Data.Entities.Enums;
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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Логин и пароль обязательны для заполнения")]
        [ObservableProperty]
        private string login;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Логин и пароль обязательны для заполнения")]
        [ObservableProperty]
        private string password;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Нам нужно знать как к вам обращаться")]
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private Roles role;

        [RelayCommand]
        public async Task SignUp()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                var error = GetErrors().First();

                _snackbarService.Show("Так не пойдет", error.ErrorMessage, Wpf.Ui.Controls.ControlAppearance.Danger);

                return;
            }

            if (!ValidatePassword())
            {
                _snackbarService.Show("Так не пойдет", "В пароле должна быть хотя-бы одна цифра", Wpf.Ui.Controls.ControlAppearance.Danger);

                return;
            }

            var signUnDto = new SignUpDTO()
            {
                Login = Login,
                RoleId = (int)Role,
                Username = Username,
                Password = Password
            };

            var result = await _authenticationService.SignUpAsync(signUnDto);

            if (result.IsFailed)
            {
                if (result.Errors.Count != 0)
                    _snackbarService.Show("Упс", result.Errors.First().Message, Wpf.Ui.Controls.ControlAppearance.Danger);
                else
                    _snackbarService.Show("Упс", "У нас не получилось тебя зарегистрировать, попробуй чуть позже", Wpf.Ui.Controls.ControlAppearance.Danger);

                return;
            }

            var session = await _authenticationService.SignInAsync(new SignInDTO()
            {
                Login = Login,
                Password = Password
            });

            _sessionManager.SetSession(session.Value);

            Authenticated = true;

            _snackbarService.Show("Добро пожаловать в семью", "Let's celebrate and eat some chick", Wpf.Ui.Controls.ControlAppearance.Success);
        }

        private bool ValidatePassword()
        {
            //i'm lazy to regex ASF

            return Password.Any(p => char.IsDigit(p));
        }


        [RelayCommand]
        public void ChangeSelectedRole(Roles role)
        {
            Role = role;
        }
    }
}