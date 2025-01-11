using System.Collections.ObjectModel;
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
    public partial class RestaurantVerificationViewModel : ViewModelBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly Services.INavigationService _navigationService;
        private readonly ISnackbarService _snackbarService;
        public RestaurantVerificationViewModel(IRestaurantService restaurantService,
                                               Services.INavigationService navigationService,
                                               ISnackbarService snackbarService,
                                               ISessionManager sessionManager)
        {
            _restaurantService = restaurantService;
            _navigationService = navigationService;
            _snackbarService = snackbarService;

            _userId = sessionManager.GetSessionInfo().UserId;
        }

        private readonly int _userId;

        [ObservableProperty]
        private string restaurantName;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private bool approved;

        [ObservableProperty]
        private byte[]? image;

        [ObservableProperty]
        private byte[]? menu;

        [ObservableProperty]
        private TimeOnly beginWorkTime;

        [ObservableProperty]
        private TimeOnly endWorkTime;

        [ObservableProperty]
        private ObservableCollection<RestaurantTagDTO> tags = [];

        [ObservableProperty]
        private bool verified;

        private int _restaurantId;
        public async Task LoadRestaurantInfo(int restaurantId)
        {
            _restaurantId = restaurantId;

            var result = await _restaurantService.GetRestaurantInfo(_restaurantId);

            if (result.IsFailed)
            {
                _snackbarService.Show("Ошибка", "Что-то пошло не так", Wpf.Ui.Controls.ControlAppearance.Danger);

                await _navigationService.NavigateBackAsync();

                return;
            }

            var info = result.Value;

            RestaurantName = info.Name;
            Description = info.Description;
            Image = info.Image;
            Menu = info.Menu;
            Verified = info.Approved;
            Description = info.Description;
            BeginWorkTime = info.BeginWorkTime;
            EndWorkTime = info.EndWorkTime;
            Verified = info.Approved;

            foreach (var tag in info.Tags)
            {
                Tags.Add(tag);
            }
        }


        async partial void OnVerifiedChanged(bool value)
        {
            await _restaurantService.ChangeRestaurantApproval(new ChangeRestaurantApprovalDTO
            {
                Approval = Verified,
                RestaurantId = _restaurantId,
                UserId = _userId
            });
        }

        [RelayCommand]
        public async Task CloseRestaurantVerification()
        {
            await _navigationService.NavigateBackAsync();
        }
    }
}
