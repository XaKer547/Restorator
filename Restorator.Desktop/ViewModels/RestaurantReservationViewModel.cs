using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Dialogs;
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
        private TimeOnly reservationStartTime;

        [ObservableProperty]
        private TimeOnly reservationEndTime;

        [ObservableProperty]
        private DateTime selectedDate = DateTime.Today;

        [ObservableProperty]
        private bool isToday = true;

        [ObservableProperty]
        private bool canSearchReserve = true;

        [ObservableProperty]
        private int hours;

        [ObservableProperty]
        private TimeOnly beginWorkTime;

        private TimeOnly _beginWorkTimeBuffer;

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

            CheckReservationSearchAvaibility();
        }

        private int _restaurantId;

        private readonly int _userId;
        public async Task LoadRestaurantPlan(int restaurantId)
        {
            _restaurantId = restaurantId;

            var result = await _reservationService.GetRestaurantPlan(BuildRestaurantPlanQuery());

            if (result.IsFailed)
            {
                await _navigationService.NavigateBackAsync();

                _snackbarService.Show("Ой", "Что-то пошло не так", ControlAppearance.Danger);

                return;
            };

            var plan = result.Value;

            Plan = plan.Scheme;

            _beginWorkTimeBuffer = plan.BeginWorkTime;

            if (IsToday)
                BeginWorkTime = TimeOnly.FromDateTime(DateTime.Now);
            else
                BeginWorkTime = _beginWorkTimeBuffer;

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

            await RefreshReservationPlanCommand.ExecuteAsync(null);
        }

        async partial void OnReservationStartTimeChanged(TimeOnly value)
        {
            await RefreshReservationPlanCommand.ExecuteAsync(null);
        }

        async partial void OnReservationEndTimeChanged(TimeOnly value)
        {
            await RefreshReservationPlanCommand.ExecuteAsync(null);
        }

        partial void OnIsTodayChanged(bool value)
        {
            if (IsToday)
                BeginWorkTime = TimeOnly.FromDateTime(DateTime.Now);
            else
                BeginWorkTime = _beginWorkTimeBuffer;

            CheckReservationSearchAvaibility();
        }

        partial void OnReservationStartTimeChanging(TimeOnly value)
        {
            //if (ReservationEndTime <= value)
            //    ReservationEndTime = value;
        }

        private void CheckReservationSearchAvaibility()
        {
            if (!IsToday)
                return;

            DateTime endWork = DateTime.Today;

            if (EndWorkTime <= BeginWorkTime)
            {
                endWork = endWork.AddDays(1);
            }

            endWork.Add(EndWorkTime.ToTimeSpan());

            CanSearchReserve = (IsToday && DateTime.Now < endWork) ^ !IsToday;
        }

        [RelayCommand(CanExecute = nameof(CanSearchReserve), AllowConcurrentExecutions = false)]
        public async Task RefreshReservationPlan()
        {
            DateTime reservationEndDate = SelectedDate;

            if (EndWorkTime <= BeginWorkTime && ReservationStartTime > ReservationEndTime)
                reservationEndDate = reservationEndDate.AddDays(1);

            var result = await _reservationService.GetRestaurantPlan(BuildRestaurantPlanQuery());

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

        private GetRestaurantPlanDTO BuildRestaurantPlanQuery()
        {
            DateTime reservationEndDate = SelectedDate;

            if (EndWorkTime <= BeginWorkTime && ReservationStartTime > ReservationEndTime)
                reservationEndDate = reservationEndDate.AddDays(1);

            return new GetRestaurantPlanDTO()
            {
                ReservationStartDate = SelectedDate.Add(ReservationStartTime.ToTimeSpan()),
                ReservationEndDate = reservationEndDate.Add(ReservationEndTime.ToTimeSpan()),
                RestaurantId = _restaurantId,
                UserId = _userId,
            };
        }


        //pass Date from filter? Get today and now

        //TODO:
        // сортировка по количеству бронирований?
        // бронирование с какого-то времени на n часов!!

        private readonly List<int> _reservedTables = [];
        [RelayCommand(CanExecute = nameof(CanSearchReserve))]
        public async void TableReservation(TableModel table)
        {
            if (table.State == Domain.Models.Enums.TableStates.Avaible)
            {
                _reservedTables.Add(table.Id);

                table.State = Domain.Models.Enums.TableStates.OccupiedByUser;

                return;
            }

            if (table.State == Domain.Models.Enums.TableStates.OccupiedByUser)
            {
                if (_reservedTables.Any(t => t == table.Id))
                {
                    var confirm = await _contentDialogService.ShowSimpleDialogAsync(new SimpleContentDialogCreateOptions()
                    {
                        Title = "Отмена бронирования стола",
                        Content = "Вы действительно хотите отменить бронь?",
                        PrimaryButtonText = "Да, хочу",
                        CloseButtonText = "Нет, я передумал"
                    });

                    if (confirm != ContentDialogResult.Primary)
                        return;

                    var result = await _reservationService.CancelReservation(new CancelReservationDTO()
                    {
                        RestaurantId = _restaurantId,
                        UserId = _userId,
                        TableId = table.Id,


                    });

                    if (result.IsFailed)
                    {
                        _snackbarService.Show("Ой", "Что-то пошло не так", ControlAppearance.Danger);

                        return;
                    };
                }
                else
                    _reservedTables.Remove(table.Id);

                table.State = Domain.Models.Enums.TableStates.Avaible;

                return;
            }
        }

        [RelayCommand(CanExecute = nameof(CanSearchReserve))]
        public async Task ConfirmTableReservation()
        {
            if (_reservedTables.Count == 0)
            {
                _snackbarService.Show("Так не пойдет", "Вы должны выбрать хотя-бы один стол для бронирования", ControlAppearance.Danger);
                return;
            }

            var dialog = new ConfirmReservationReservationContentDialog(new ConfirmRestaurantReservationModel()
            {
                ReservationStart = ReservationStartTime,
                ReservationEnd = ReservationEndTime,
                TablesCount = _reservedTables.Count,
            });

            var confirmation = await _contentDialogService.ShowAsync(dialog, new CancellationToken());

            if (confirmation != ContentDialogResult.Primary)
                return;

            DateTime reservationEndDate = SelectedDate;

            if (EndWorkTime <= BeginWorkTime)
            {
                reservationEndDate = reservationEndDate.AddDays(1);
            }

            var reservation = new CreateRestaurantReservationDTO
            {
                UserId = _userId,
                RestaurantId = _restaurantId,
                ReservedTables = _reservedTables,
                ReservationStartDate = SelectedDate.Add(ReservationStartTime.ToTimeSpan()),
                ReservationEndDate = reservationEndDate.Add(ReservationEndTime.ToTimeSpan()),
            };

            var result = await _reservationService.ReserveTables(reservation);

            if (result.IsFailed)
            {
                _snackbarService.Show("Ой", "Что-то пошло не так", ControlAppearance.Danger);

                return;
            };

            _snackbarService.Show("Ура!", "Будем ждать вас к назначенному времени!", ControlAppearance.Success);

            _reservedTables.Clear();
        }


        [RelayCommand]
        public async Task CloseRestaurantReservation()
        {
            await _navigationService.NavigateBackAsync();
        }
    }
}