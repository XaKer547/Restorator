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
        #region Dependencies
        public static readonly DependencyProperty SelectedTimeProperty = DependencyProperty.Register(
         "SelectedTime", typeof(TimeOnly?), typeof(TimePicker), new FrameworkPropertyMetadata(TimeOnly.FromDateTime(DateTime.Now), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedTimeChanged));

        public static readonly DependencyProperty MinuteIntervalProperty = DependencyProperty.Register(
            "MinuteInterval", typeof(int), typeof(TimePicker), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnMinuteIntervalChanged));

        public static readonly DependencyProperty HourProperty = DependencyProperty.Register(
            "Hour", typeof(int), typeof(TimePicker), new FrameworkPropertyMetadata(DateTime.Now.Hour, OnHourChanged));

        public static readonly DependencyProperty MinuteProperty = DependencyProperty.Register(
            "Minute", typeof(int), typeof(TimePicker), new FrameworkPropertyMetadata(DateTime.Now.Minute, OnMinuteChanged));

        public static readonly DependencyProperty MaxTimeProperty = DependencyProperty.Register(
            "EndTime", typeof(TimeOnly), typeof(TimePicker), new FrameworkPropertyMetadata(TimeOnly.Parse("12:00"), OnEndTimeChanged));

        public static readonly DependencyProperty MinTimeProperty = DependencyProperty.Register(
            "StartTime", typeof(TimeOnly), typeof(TimePicker), new FrameworkPropertyMetadata(TimeOnly.MinValue, OnStartTimeChanged));
        #endregion

        private bool _isUpdatingTime = false;
        private DispatcherTimer? _timer;
        private int _currentChange = 0;

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

        public TimeOnly SelectedTime
        {
            get { return (TimeOnly)GetValue(SelectedTimeProperty); }
            set { SetValue(SelectedTimeProperty, value); }
        }

        public TimeOnly EndTime
        {
            get { return (TimeOnly)GetValue(MaxTimeProperty); }
            set { SetValue(MaxTimeProperty, value); }
        }

        public TimeOnly StartTime
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
            set
            {
                SetValue(MinuteProperty, value);
            }
        }

        public int MinuteInterval
        {
            get { return (int)GetValue(MinuteIntervalProperty); }
            set { SetValue(MinuteIntervalProperty, value); }
        }

        private static void OnStartTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)d;

            TimeOnly interval = (TimeOnly)e.NewValue;

            timePicker.SetValue(MinTimeProperty, interval);

            UpdateTime(timePicker);
        }

        private static void OnEndTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)d;

            TimeOnly interval = (TimeOnly)e.NewValue;

            timePicker.SetValue(MaxTimeProperty, interval);

            UpdateTime(timePicker);
        }

        private static void OnMinuteIntervalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)d;

            int interval = (int)e.NewValue;

            timePicker.SetValue(MinuteIntervalProperty, interval);

            timePicker.Minute += interval - (timePicker.Minute % interval);
        }

        private static void OnSelectedTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)d;

            if (!timePicker._isUpdatingTime)
            {
                int hour = timePicker.InHourBounds(timePicker.SelectedTime.Hour, ((TimeOnly)e.OldValue).Hour);

                int minute = timePicker.SelectedTime.Minute;

                if (timePicker.Hour != hour)
                    minute = 0;
                else
                    minute = timePicker.Minute;

                timePicker.Hour = hour;

                timePicker.Minute = minute;
            }
        }

        private static void OnHourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimePicker timePicker = (TimePicker)d;

            int hour = timePicker.InHourBounds((int)e.NewValue, (int)e.OldValue);

            timePicker.SetValue(HourProperty, hour);

            if (!timePicker._isUpdatingTime)
            {
                UpdateTime(timePicker);
            }
        }

        private bool InHourBounds(int hour)
        {
            if (hour >= 24)
                hour = 0;
            else if (hour < 0)
                hour = 23;

            bool inBounds;

            if (StartTime < EndTime)
                inBounds = !(hour >= StartTime.Hour && hour <= EndTime.Hour);
            else
                inBounds = hour >= StartTime.Hour || hour <= EndTime.Hour;

            return inBounds;
        }
        private int InHourBounds(int newValue, int oldValue)
        {
            if (newValue >= 24)
                newValue = 0;
            else if (newValue < 0)
                newValue = 23;

            if (InHourBounds(newValue))
                return newValue;

            if (newValue < oldValue)
                return EndTime.Hour;
            else
                return StartTime.Hour;
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

            minute += (minute % timePicker.MinuteInterval);

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