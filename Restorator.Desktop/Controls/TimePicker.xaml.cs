using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Restorator.Desktop.Controls
{
    /// <summary>
    /// Логика взаимодействия для TimePicker.xaml
    /// </summary>
    public partial class TimePicker : UserControl
    {
        public static readonly DependencyProperty SelectedTimeProperty = DependencyProperty.Register(
            "SelectedTime", typeof(TimeOnly?), typeof(TimePicker), new FrameworkPropertyMetadata(TimeOnly.FromDateTime(DateTime.Now), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedTimeChanged));

        public static readonly DependencyProperty MinuteIntervalProperty = DependencyProperty.Register(
            "MinuteInterval", typeof(int), typeof(TimePicker), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnMinuteIntervalChanged));

        public static readonly DependencyProperty HourProperty = DependencyProperty.Register(
            "Hour", typeof(int), typeof(TimePicker), new FrameworkPropertyMetadata(DateTime.Now.Hour, OnHourChanged));

        public static readonly DependencyProperty MinuteProperty = DependencyProperty.Register(
            "Minute", typeof(int), typeof(TimePicker), new FrameworkPropertyMetadata(DateTime.Now.Minute, OnMinuteChanged));

        public static readonly DependencyProperty MaxTimeProperty = DependencyProperty.Register(
            "MaxTime", typeof(TimeOnly), typeof(TimePicker), new FrameworkPropertyMetadata(TimeOnly.Parse("12:00"), OnMaxTimeChanged));

        public static readonly DependencyProperty MinTimeProperty = DependencyProperty.Register(
            "MinTime", typeof(TimeOnly), typeof(TimePicker), new FrameworkPropertyMetadata(TimeOnly.MinValue, OnMinTimeChanged));

        private bool _isUpdatingTime = false;
        private DispatcherTimer? _timer;
        private int _currentChange = 0;

        public TimeOnly SelectedTime
        {
            get { return (TimeOnly)GetValue(SelectedTimeProperty); }
            set { SetValue(SelectedTimeProperty, value); }
        }

        public TimeOnly MaxTime
        {
            get { return (TimeOnly)GetValue(MaxTimeProperty); }
            set { SetValue(MaxTimeProperty, value); }
        }

        public TimeOnly MinTime
        {
            get { return (TimeOnly)GetValue(MinTimeProperty); }
            set { SetValue(MinTimeProperty, value); }
        }

        public int Hour
        {
            get { return (int)GetValue(HourProperty); }
            set { SetValue(HourProperty, value); }
        }

        public int Minute
        {
            get { return (int)GetValue(MinuteProperty); }
            set { SetValue(MinuteProperty, value); }
        }

        public int MinuteInterval
        {
            get { return (int)GetValue(MinuteIntervalProperty); }
            set { SetValue(MinuteIntervalProperty, value); }
        }

        static TimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimePicker), new FrameworkPropertyMetadata(typeof(TimePicker)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Button hourUpButton = GetTemplateChild("HourUpButton") as Button;
            Button hourDownButton = GetTemplateChild("HourDownButton") as Button;
            Button minuteUpButton = GetTemplateChild("MinuteUpButton") as Button;
            Button minuteDownButton = GetTemplateChild("MinuteDownButton") as Button;
            Border hourBorder = GetTemplateChild("HourBorder") as Border;
            Border minuteBorder = GetTemplateChild("MinuteBorder") as Border;

            if (hourUpButton != null)
            {
                hourUpButton.Click += (sender, args) => ChangeHour(1);
                hourUpButton.PreviewMouseLeftButtonDown += (sender, args) => StartTimer(1);
                hourUpButton.PreviewMouseLeftButtonUp += (sender, args) => StopTimer();
                hourUpButton.MouseLeave += (sender, args) => StopTimer();
            }

            if (hourDownButton != null)
            {
                hourDownButton.Click += (sender, args) => ChangeHour(-1);
                hourDownButton.PreviewMouseLeftButtonDown += (sender, args) => StartTimer(-1);
                hourDownButton.PreviewMouseLeftButtonUp += (sender, args) => StopTimer();
                hourDownButton.MouseLeave += (sender, args) => StopTimer();
            }

            if (minuteUpButton != null)
            {
                minuteUpButton.Click += (sender, args) => ChangeMinute(MinuteInterval);
                minuteUpButton.PreviewMouseLeftButtonDown += (sender, args) => StartTimer(2);
                minuteUpButton.PreviewMouseLeftButtonUp += (sender, args) => StopTimer();
                minuteUpButton.MouseLeave += (sender, args) => StopTimer();
            }

            if (minuteDownButton != null)
            {
                minuteDownButton.Click += (sender, args) => ChangeMinute(-MinuteInterval);
                minuteDownButton.PreviewMouseLeftButtonDown += (sender, args) => StartTimer(-2);
                minuteDownButton.PreviewMouseLeftButtonUp += (sender, args) => StopTimer();
                minuteDownButton.MouseLeave += (sender, args) => StopTimer();
            }

            if (hourBorder != null)
            {
                hourBorder.MouseWheel += OnHourMouseWheel;
            }

            if (minuteBorder != null)
            {
                minuteBorder.MouseWheel += OnMinuteMouseWheel;
            }
        }

        private static void OnMinTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)d;

            TimeOnly interval = (TimeOnly)e.NewValue;

            timePicker.SetValue(MinTimeProperty, interval);
        }

        private static void OnMaxTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)d;

            TimeOnly interval = (TimeOnly)e.NewValue;

            timePicker.SetValue(MaxTimeProperty, interval);
        }

        private static void OnMinuteIntervalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)d;

            int interval = (int)e.NewValue;

            timePicker.SetValue(MinuteIntervalProperty, interval);
        }

        private static void OnSelectedTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)d;

            if (!timePicker._isUpdatingTime)
            {
                timePicker.Hour = timePicker.SelectedTime.Hour;
                timePicker.Minute = timePicker.SelectedTime.Minute;
            }
        }

        private static void OnHourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)d;
            int hour = (int)e.NewValue;
            if (hour > timePicker.MaxTime.Hour)
            {
                hour = timePicker.MinTime.Hour;
            }
            else if (hour < timePicker.MinTime.Hour)
            {
                hour = timePicker.MaxTime.Hour;
            }
            timePicker.SetValue(HourProperty, hour);
            if (!timePicker._isUpdatingTime)
            {
                UpdateTime(timePicker);
            }
        }

        private static void OnMinuteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)d;
            int minute = (int)e.NewValue;

            var cap = 60 - timePicker.MinuteInterval;

            if (minute < 0)
            {
                minute = cap;

                if (!timePicker._isUpdatingTime)
                {
                    timePicker._isUpdatingTime = true;

                    timePicker.ChangeHour(-1);

                    timePicker._isUpdatingTime = false;
                }
            }
            else if (minute > cap)
            {
                minute = 0;

                if (!timePicker._isUpdatingTime)
                {
                    timePicker._isUpdatingTime = true;

                    timePicker.ChangeHour(1);

                    timePicker._isUpdatingTime = false;
                }
            }
            timePicker.SetValue(MinuteProperty, minute);
            if (!timePicker._isUpdatingTime)
            {
                UpdateTime(timePicker);
            }
        }

        private void StartTimer(int changeType)
        {
            _currentChange = changeType;
            if (_timer != null)
            {
                return;
            }

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(150);
            _timer.Tick += TimerTick;
            _timer.Start();
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            if (_currentChange == 1) { ChangeHour(1); }
            else if (_currentChange == -1) { ChangeHour(-1); }
            else if (_currentChange == 2) { ChangeMinute(1); }
            else if (_currentChange == -2) { ChangeMinute(-1); }
        }
        private void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer = null;
                _currentChange = 0;
            }
        }

        private void ChangeHour(int amount)
        {
            SetValue(HourProperty, Hour + amount);
        }

        private void ChangeMinute(int amount)
        {
            SetValue(MinuteProperty, Minute + amount);
        }
        private static void UpdateTime(TimePicker timePicker)
        {
            if (!timePicker._isUpdatingTime)
            {
                timePicker.SelectedTime = new TimeOnly(timePicker.Hour, timePicker.Minute);
                timePicker._isUpdatingTime = false;
            }
        }

        private void OnMinuteMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                ChangeMinute(MinuteInterval);
            }
            else
            {
                ChangeMinute(-MinuteInterval);
            }
        }

        private void OnHourMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                ChangeHour(1);
            }
            else
            {
                ChangeHour(-1);
            }
        }
    }
}