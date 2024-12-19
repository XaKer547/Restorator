using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Services;

namespace Restorator.Desktop.ViewModels
{
    public partial class SignUpViewModel : AuthenticationViewModelBase
    {
        private readonly IMed


        private readonly IAuthenticationService _authenticationService;
        private readonly ISessionManager _sessionManager;

        public SignUpViewModel(IAuthenticationService authenticationService, ISessionManager sessionManager)
        {
            _authenticationService = authenticationService;
            _sessionManager = sessionManager;
        }

        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string password;

        [RelayCommand]
        public async Task SignUp()
        {
            var signInDto = new SignUpDTO()
            {
                Login = Login,
                Password = Password
            };

            var result = await _authenticationService.SignUpAsync(signInDto);

            if (!result)
            {
                //TODO:
                //как подгружать view, чтобы родитель получил событие 
                //привязать окно к навигатору и отправить в дочерку? 

                return;
            }

            var session = await _authenticationService.SignInAsync(new SignInDTO()
            {
                Login = Login,
                Password = Password
            });

            _sessionManager.SetSession(session.SessionInfo!);

            Authenticated = true;
        }
    }
}