using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Models;
using Restorator.Desktop.ViewModels.Abstract;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantTemplateGeneratorViewModel : ViewModelBase
    {
        private const string SchemeLocation = "C:\\Users\\user\\source\\repos\\XaKer547\\Restorator\\Restorator.Seeder\\Resources\\RestaurantsPlan";

        [ObservableProperty]
        private ObservableCollection<TableModel> tables = [];

        [ObservableProperty]
        private ObservableCollection<byte[]> templates = [];

        [ObservableProperty]
        private TableModel? selectedTable;

        [RelayCommand]
        public void Initialize()
        {
            Templates.Clear();

            Tables.Clear();

            AddNewTable();

            var images = Directory.GetFiles(SchemeLocation);

            foreach (var image in images)
            {
                Templates.Add(GetByteArrayFromImage(image));
            }
        }

        [RelayCommand]
        public void ChangeSelectedTable(TableModel table)
        {
            SelectedTable = table;
        }

        [RelayCommand]
        public void AddNewTable()
        {
            var table = new TableModel()
            {
                X = 366.09,
                Y = 592.63,
                State = Domain.Models.Enums.TableStates.OccupiedByUser,
                Rotation = 0,
                Height = 183,
                Width = 183,
            };

            SelectedTable = table;

            Tables.Add(table);
        }

        [RelayCommand]
        public void RemoveTable()
        {
            Tables.Remove(SelectedTable);

            SelectedTable = null;
        }

        private byte[] GetByteArrayFromImage(string imagePath)
        {
            var stream = File.OpenRead(imagePath);

            using var memoryStream = new MemoryStream();

            stream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
