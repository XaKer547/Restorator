using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Services;
using Wpf.Ui;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantEditorViewModelBase : ViewModelBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly Services.INavigationService _navigationService;
        private readonly IContentDialogService _contentDialogService;
        public RestaurantEditorViewModelBase(IRestaurantService restaurantService,
                                         Services.INavigationService navigationService,
                                         ISessionManager sessionManager,
                                         IContentDialogService contentDialogService)
        {
            _restaurantService = restaurantService;
            _navigationService = navigationService;
            _userId = sessionManager.GetSessionInfo().UserId;

            _contentDialogService = contentDialogService;
        }

        protected readonly int _userId;

        [ObservableProperty]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Название ресторана обязательно для заполнения")]
        private string restaurantName;

        [ObservableProperty]
        [Required(AllowEmptyStrings = false, ErrorMessage = "У ресторана должно быть описание")]
        private string description;

        [ObservableProperty]
        private bool approved;

        [ObservableProperty]
        private byte[]? image;

        [ObservableProperty]
        private byte[]? menu;

        [ObservableProperty]
        private DateTime beginWorkTime;

        [ObservableProperty]
        private DateTime endWorkTime;

        [ObservableProperty]
        [MinLength(1, ErrorMessage = "Должен быть выбран хотя-бы один тэг")]
        private ObservableCollection<RestaurantTagDTO> restaurantTags = [];

        [ObservableProperty]
        private ObservableCollection<RestaurantTagDTO> tags = [];

        [RelayCommand]
        public async Task LoadRestaurantTags()
        {
            var tags = await _restaurantService.GetRestaurantTags();

            foreach (var tag in tags)
            {
                if (!Tags.Any(t => t.Id == tag.Id))
                    Tags.Add(tag);
            }
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

        [RelayCommand]
        public async Task CloseRestaurantEditor()
        {
            await _navigationService.NavigateBackAsync();
        }
    }
}
