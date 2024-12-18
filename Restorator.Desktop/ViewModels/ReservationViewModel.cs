using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Dialogs;
using Restorator.Desktop.Models;
using Restorator.Desktop.ViewModels.Abstract;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Restorator.Desktop.ViewModels
{
    public partial class ReservationViewModel : ViewModelBase
    {
        //private readonly IReservationService _reservationService;

        private readonly IContentDialogService _contentDialogService;

        [ObservableProperty]
        private ObservableCollection<TableModel> _tables = [];

        public ReservationViewModel(IContentDialogService contentDialogService)
        {
            _contentDialogService = contentDialogService;

            Tables.Add(new TableModel()
            {
                Id = 1,
                X = 366.09F,
                Y = 592.63F,
                Height = 183,
                Width = 183,
                State = Domain.Models.Enums.TableStates.OccupiedByUser,
            });
        }

        [RelayCommand]
        public async Task TableReservation(TableModel table)
        {
            if (table.State == Domain.Models.Enums.TableStates.Avaible)
            {
                await ConfirmTableReservation(table);
                return;
            }

            if (table.State == Domain.Models.Enums.TableStates.OccupiedByUser)
            {
                await CancelTableReservation(table);
                return;
            }
        }

        [RelayCommand]
        public async Task ConfirmTableReservation(TableModel table)
        {
            var dialog = new TableReservationContentDialog();

            var result = await _contentDialogService.ShowAsync(dialog, new CancellationToken());

            if (result == ContentDialogResult.Primary)
            {
                table.State = Domain.Models.Enums.TableStates.OccupiedByUser;

                return;
            }
        }

        [RelayCommand]
        public async Task CancelTableReservation(TableModel table)
        {
            var dialog = new CancelTableReservationDialog();

            var result = await _contentDialogService.ShowAsync(dialog, new CancellationToken());

            if (result == ContentDialogResult.Primary)
            {
                table.State = Domain.Models.Enums.TableStates.Avaible;

                return;
            }
        }
    }
}