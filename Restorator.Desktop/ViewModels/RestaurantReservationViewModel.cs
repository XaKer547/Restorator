using System.Collections.Immutable;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Models;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Services;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantReservationViewModel : RestaurantViewModelBase
    {
        private readonly IReservationService _reservationService;
        private readonly IContentDialogService _contentDialogService;
        private readonly ISnackbarService _snackbarService;
        private readonly Services.INavigationService _navigationService;

        [ObservableProperty]
        private IReadOnlyCollection<TableModel> _tables = [];

        [ObservableProperty]
        private byte[] plan;

        [ObservableProperty]
        private TimeOnly selectedTime;

        [ObservableProperty]
        private TimeOnly beginWorkTime;

        [ObservableProperty]
        private TimeOnly endWorkTime;

        public RestaurantReservationViewModel(IContentDialogService contentDialogService,
                                    IReservationService reservationService,
                                    Services.INavigationService navigationService,
                                    ISnackbarService snackbarService)
        {
            _contentDialogService = contentDialogService;
            _reservationService = reservationService;
            _navigationService = navigationService;
            _snackbarService = snackbarService;
        }

        private int _restaurantId;

        [RelayCommand]
        public async Task LoadReservationPlan(int restaurantId)
        {
            if (_restaurantId == restaurantId)
                return;

            _restaurantId = restaurantId;

            var result = await _reservationService.GetRestaurantPlan(_restaurantId);

            if (result.IsFailed)
            {
                _snackbarService.Show("Ой", "Что-то пошло не так", ControlAppearance.Danger);

                _navigationService.Navigate<RestaurantSearchViewModel>();

                return;
            };

            var plan = result.Value;

            Plan = plan.Scheme;

            BeginWorkTime = plan.BeginWorkTime;

            EndWorkTime = plan.EndWorkTime;

            Tables = plan.Tables.Select(t => new TableModel
            {
                Id = t.Id,
                X = t.X,
                Y = t.Y,
                Width = t.Width,
                Height = t.Height,
                Rotation = t.Rotation,
                State = Domain.Models.Enums.TableStates.Avaible,
            }).ToImmutableList();
        }

        //TODO:
        // Фото Меню +-
        // Фото ресторана +-
        // сортировка по количеству бронирований
        // бронирование с какого-то времени на n часов
        [RelayCommand]
        public void TableReservation(TableModel table)
        {
            if (table.State == Domain.Models.Enums.TableStates.Avaible)
            {
                table.State = Domain.Models.Enums.TableStates.OccupiedByUser;

                return;
            }

            if (table.State == Domain.Models.Enums.TableStates.OccupiedByUser)
            {
                table.State = Domain.Models.Enums.TableStates.Avaible;
                return;
            }
        }

        [RelayCommand]
        public async Task ConfirmTableReservation(TableModel table)
        {
            // сколько можно бронировать за раз?
        }
    }
}