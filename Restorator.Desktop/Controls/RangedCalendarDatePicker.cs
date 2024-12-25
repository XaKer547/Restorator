using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace Restorator.Desktop.Controls
{
    public partial class RangedCalendarDatePicker : CalendarDatePicker
    {
        public RangedCalendarDatePicker()
        {
            var d = new DatePicker();

            d.DisplayDateStart = DateTime.Now;
        }

    }
}
