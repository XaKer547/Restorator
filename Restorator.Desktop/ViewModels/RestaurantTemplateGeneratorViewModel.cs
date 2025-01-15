using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Models;
using Restorator.Desktop.ViewModels.Abstract;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantTemplateGeneratorViewModel : ViewModelBase
    {
        private const string SchemeLocation =
            "F:\\Restorator\\Restorator.Seeder\\Resources\\RestaurantsPlan";

        //тут свой путь к сидеру пиши

        [ObservableProperty]
        private ObservableCollection<TableModel> tables = [];

        [ObservableProperty]
        private ObservableCollection<TemplateModel> templates = [];

        [ObservableProperty]
        private TableModel? selectedTable;

        [ObservableProperty]
        private TemplateModel selectedTemplate;

        [ObservableProperty]
        private string seederScript;

        [ObservableProperty]
        private bool canChangeTable = false;

        [RelayCommand]
        public void Initialize()
        {
            Templates.Clear();

            Tables.Clear();

            AddNewTable();

            var images = Directory.GetFiles(SchemeLocation);

            foreach (var image in images)
            {
                Templates.Add(GetTemplateFromPath(image));
            }
        }

        [RelayCommand]
        public void ChangeSelectedTable(TableModel table)
        {
            SelectedTable = table;

            CanChangeTable = true;
        }

        [RelayCommand]
        public void AddNewTable()
        {
            var table = new TableModel()
            {
                X = 0,
                Y = 0,
                State = Domain.Models.Enums.TableStates.OccupiedByUser,
                Rotation = 0,
                Height = 183,
                Width = 183,
            };

            SelectedTable = table;

            CanChangeTable = true;

            Tables.Add(table);
        }

        [RelayCommand]
        public void ClearScheme()
        {
            Tables.Clear();
        }

        [RelayCommand]
        public void SetSelectedTableMiniSquare()
        {
            if (!CanChangeTable)
                return;

            SelectedTable!.Width = 100;
            SelectedTable.Height = 108;
        }

        [RelayCommand]
        public void RemoveTable()
        {
            Tables.Remove(SelectedTable!);

            SelectedTable = null;

            CanChangeTable = false;
        }

        [RelayCommand]
        public void SetSelectedTableSquare()
        {
            if (!CanChangeTable)
                return;

            SelectedTable!.Width = 183;
            SelectedTable.Height = 183;
        }

        [RelayCommand]
        public void SetSelectedTableRectanle()
        {
            if (!CanChangeTable)
                return;

            SelectedTable!.Width = 183;
            SelectedTable.Height = 110;
        }

        private TemplateModel GetTemplateFromPath(string imagePath)
        {
            return new TemplateModel()
            {
                Filename = Path.GetFileName(imagePath),
                Content = File.ReadAllBytes(imagePath),
            };
        }

        [RelayCommand]
        public void CopyScriptToBuffer() => Clipboard.SetText(SeederScript);

        [RelayCommand]
        public void GenerateSeederScript()
        {
            var script = new StringBuilder("new RestaurantTemplate\n{\n");

            var fileNameWithoutExtension = SelectedTemplate.Filename.Split('.')[0];

            script.AppendLine(
                $"\tImage = EmbeddedResourceHelper.GetRestaurantPlan(\"{fileNameWithoutExtension}\"),"
            );

            script.AppendLine("\tTables = new List<Table>()\n\t\t{");

            foreach (var table in Tables)
            {
                script.AppendLine("\t\t\tnew Table()\n\t\t\t{");

                int templateId;

                if (table.Width == 183 && table.Height == 183) // надо бы сделать это по другому(
                    templateId = 1;
                else if (table.Width == 183 && table.Height == 110)
                    templateId = 4;
                else
                    templateId = 7;

                if (table.Rotation == 45)
                    templateId++;
                else if (table.Rotation == 90)
                    templateId += 2;

                script.AppendLine($"\t\t\t\tTableTemplateId = {templateId},");

                //у меня margin стоит для того чтобы FlipView отработал
                script.AppendLine(
                    CultureInfo.InvariantCulture,
                    $"\t\t\t\tX = {Math.Round(table.X, 2) + 10}F,"
                );

                script.AppendLine(
                    CultureInfo.InvariantCulture,
                    $"\t\t\t\tY = {Math.Round(table.Y, 2)}F,"
                );

                script.AppendLine("\t\t\t},");
            }

            script.AppendLine("\t\t}");
            script.AppendLine("},");

            SeederScript = script.ToString();
        }
    }
}
