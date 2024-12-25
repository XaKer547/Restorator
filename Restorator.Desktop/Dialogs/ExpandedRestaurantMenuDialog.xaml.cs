using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
