using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Restorator.Desktop.Services;
using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantEditorViewModel : ViewModelBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly INavigationService _navigationService;
        public RestaurantEditorViewModel(IRestaurantService restaurantService, ISessionManager sessionManager)
        {
            _restaurantService = restaurantService;

            _userId = sessionManager.GetSessionInfo().UserId;

            ApplyChangesCommand = CreateRestaurantCommand;
        }

        private readonly int _userId;

        [ObservableProperty]
        private string restaurantName;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private byte[]? image;

        [ObservableProperty]
        private byte[]? menu;

        [ObservableProperty]
        private DateTime beginWorkTime;

        [ObservableProperty]
        private DateTime endWorkTime;

        [ObservableProperty]
        private ObservableCollection<RestaurantTagDTO> tags = [];

        private int _restaurantId;
        private bool EditingExistingRestaurant => _restaurantId != default;
        public async Task LoadRestaurantInfo(int restaurantId)
        {
            _restaurantId = restaurantId;

            ApplyChangesCommand = UpdateRestaurantCommand;

            var result = await _restaurantService.GetRestaurantInfo(_restaurantId);

            if (result.IsFailed)
            {

                return;
            }

            var info = result.Value;

            RestaurantName = info.Name;

            Description = info.Description;

            Image = info.Image;

            Menu = info.Menu;

            BeginWorkTime = DateTime.Today.Add(info.BeginWorkTime.ToTimeSpan());

            EndWorkTime = DateTime.Today.Add(info.EndWorkTime.ToTimeSpan());
        }

        [RelayCommand]
        public async Task LoadRestaurantTags()
        {
            Tags.Clear();

            var tags = await _restaurantService.GetRestaurantTags();

            foreach (var tag in tags)
                Tags.Add(tag);
        }

        [RelayCommand]
        public void LoadRestaurantImage()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Изображения|*.jpg;*.jpeg;*.png;"
            };


            if (dialog.ShowDialog() != true)
                return;


            Image = File.ReadAllBytes(dialog.FileName);
        }

        [RelayCommand]
        public void LoadRestaurantMenuImage()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Изображения|*.jpg;*.jpeg;*.png;"
            };


            if (dialog.ShowDialog() != true)
                return;


            Menu = File.ReadAllBytes(dialog.FileName);
        }

        [ObservableProperty]
        public ICommand applyChangesCommand;

        [RelayCommand]
        public async Task UpdateRestaurant()
        {
            var result = await _restaurantService.UpdateRestaurant(new UpdateRestraurantDTO
            {
                RestaurantId = _restaurantId,
                Name = RestaurantName,
                BeginWorkTime = TimeOnly.FromDateTime(BeginWorkTime),
                EndWorkTime = TimeOnly.FromDateTime(EndWorkTime),
                Description = Description,
                Image = Image,
                Menu = Menu,
            });
        }

        [RelayCommand]
        public async Task CreateRestaurant()
        {
            var result = await _restaurantService.CreateRestaurant(new CreateRestaurantDTO
            {
                UserId = _userId,
                Name = RestaurantName,
                BeginWorkTime = TimeOnly.FromDateTime(BeginWorkTime),
                EndWorkTime = TimeOnly.FromDateTime(EndWorkTime),
                Description = Description,
                Image = Image,
                Menu = Menu,
            });
        }

        [RelayCommand(CanExecute = nameof(EditingExistingRestaurant))]
        public async Task DeleteRestaurant()
        {
            //confirm;

            await _restaurantService.DeleteRestaurant(_restaurantId);

            await _navigationService.NavigateBackAsync();
        }

        [RelayCommand]
        public async Task CloseRestaurantEditor()
        {
            await _navigationService.NavigateBackAsync();
        }
    }
}
