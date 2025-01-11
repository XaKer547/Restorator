using Wpf.Ui.Controls;

namespace Restorator.Desktop.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ExpandedRestaurantMenuDialog.xaml
    /// </summary>
    public partial class ExpandedRestaurantMenuDialog : ContentDialog
    {
        public byte[] Image { get; set; }
        public ExpandedRestaurantMenuDialog(byte[] image)
        {
            Image = image;
            DataContext = this;
            InitializeComponent();
        }
    }
}
