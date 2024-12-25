using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Models;
using Restorator.Desktop.Session;
using Restorator.Desktop.ViewModels.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Services;
using System.Collections.Immutable;
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
        private DateTime selectedDate = DateTime.Today;

        [ObservableProperty]
        private bool isToday = true;

        [ObservableProperty]
        private int hours;

        [ObservableProperty]
        private TimeOnly beginWorkTime;

        [ObservableProperty]
        private TimeOnly endWorkTime;

        public RestaurantReservationViewModel(IContentDialogService contentDialogService,
                                    IReservationService reservationService,
                                    Services.INavigationService navigationService,
                                    ISnackbarService snackbarService,
                                    ISessionManager sessionManager)
        {
            _contentDialogService = contentDialogService;
            _reservationService = reservationService;
            _navigationService = navigationService;
            _snackbarService = snackbarService;

            _userId = 1;//sessionManager.GetSessionInfo().UserId;
        }

        private int _restaurantId;

        private readonly int _userId;
        public async Task LoadReservationPlan(int restaurantId)
        {
            _restaurantId = restaurantId;

            var result = await _reservationService.GetRestaurantPlan(new GetRestaurantPlanDTO()
            {
                //ReservationStart = new DateTime(SelectedDate, SelectedTime),
                //ReservationEnd = new DateTime(SelectedDate, SelectedTime.AddHours(Hours)),
                RestaurantId = _restaurantId,
                UserId = _userId,
            });

            if (result.IsFailed)
            {
                await _navigationService.NavigateBackAsync();

                _snackbarService.Show("Ой", "Что-то пошло не так", ControlAppearance.Danger);

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
                State = t.State,
            }).ToImmutableList();
        }

        async partial void OnSelectedDateChanged(DateTime value)
        {
            IsToday = value == DateTime.Today;

            await RefreshReservationPlan();
        }

        [RelayCommand]
        public async Task RefreshReservationPlan()
        {
            var result = await _reservationService.GetRestaurantPlan(new GetRestaurantPlanDTO()
            {
                //ReservationStart = SelectedDate new DateTime(SelectedDate, SelectedTime),
                //ReservationEnd = new DateTime(SelectedDate, SelectedTime.AddHours(Hours)),
                RestaurantId = _restaurantId,
                UserId = _userId,
            });

            if (result.IsFailed)
            {
                await _navigationService.NavigateBackAsync();

                _snackbarService.Show("Ой", "Что-то пошло не так", ControlAppearance.Danger);

                return;
            };

            var plan = result.Value;

            Tables = plan.Tables.Select(t => new TableModel
            {
                Id = t.Id,
                X = t.X,
                Y = t.Y,
                Width = t.Width,
                Height = t.Height,
                Rotation = t.Rotation,
                State = t.State,
            }).ToImmutableList();
        }

        //pass Date from filter? Get today and now

        //TODO:
        // сортировка по количеству бронирований?
        // бронирование с какого-то времени на n часов!!

        private List<int> _reservedTables = [];
        [RelayCommand]
        public void TableReservation(TableModel table)
        {
            if (table.State == Domain.Models.Enums.TableStates.Avaible)
            {
                _reservedTables.Add(table.Id);

                table.State = Domain.Models.Enums.TableStates.OccupiedByUser;

                return;
            }

            if (table.State == Domain.Models.Enums.TableStates.OccupiedByUser)
            {
                _reservedTables.Remove(table.Id);

                table.State = Domain.Models.Enums.TableStates.Avaible;

                return;
            }
        }

        [RelayCommand]
        public async Task ConfirmTableReservation()
        {
            if (_reservedTables.Count == 0)
            {
                _snackbarService.Show("Так не пойдет", "Вы должны выбрать хотя-бы один стол для бронирования", ControlAppearance.Danger);
            }

            var reservation = new CreateRestaurantReservationDTO
            {
                UserId = _userId,
                ReservedTables = _reservedTables,
                //ReservationDate = Selete,
            };

            await _reservationService.ReserveTables(reservation);

            // много можно бронировать за раз
        }

        [RelayCommand]
        public async Task CloseRestaurantReservation()
        {
            await _navigationService.NavigateBackAsync();
        }
    }
}