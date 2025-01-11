using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restorator.Desktop.Models;
using Restorator.Desktop.ViewModels.Abstract;

namespace Restorator.Desktop.ViewModels
{
    public partial class RestaurantTemplateGeneratorViewModel : ViewModelBase
    {
        private const string SchemeLocation = "C:\\Users\\user\\source\\repos\\XaKer547\\Restorator\\Restorator.Seeder\\Resources\\RestaurantsPlan";
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

            script.AppendLine($"\tImage = EmbeddedResourceHelper.GetRestaurantPlan(\"{fileNameWithoutExtension}\"),");

            script.AppendLine("\tTables = new List<Table>()\n\t\t{");

            foreach (var table in Tables)
            {
                script.AppendLine("\t\t\tnew Table()\n\t\t\t{");

                var templateId = table.Width == 183 && table.Height == 183 ? 1 : 2;

                script.AppendLine($"\t\t\t\tTableTemplateId = {templateId}");
                script.AppendLine($"\t\t\t\tX = {table.X + 10},"); //у меня margin стоит для того чтобы FlipView отработал
                script.AppendLine($"\t\t\t\tY = {table.Y}");
                script.AppendLine("\t\t\t},");
            }

            script.AppendLine("\t\t}");
            script.AppendLine("}");

            SeederScript = script.ToString();
        }
    }
}
