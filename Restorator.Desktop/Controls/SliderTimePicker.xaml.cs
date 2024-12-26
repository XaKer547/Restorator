using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Restorator.Desktop.Controls
{
    /// <summary>
    /// Логика взаимодействия для SliderTimePicker.xaml
    /// </summary>
    public partial class SliderTimePicker : UserControl
    {
        public static readonly DependencyProperty SelectedTimeProperty = DependencyProperty.Register(
            "SelectedTime",
            typeof(TimeOnly),
            typeof(SliderTimePicker),
            new FrameworkPropertyMetadata(default(TimeOnly), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedTimeChanged));

        public static readonly RoutedEvent TimeChangedEvent = EventManager.RegisterRoutedEvent("TimeChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SliderTimePicker));

        public static readonly DependencyProperty MinTimeProperty =
        DependencyProperty.Register(
            "StartTime",
            typeof(TimeOnly),
            typeof(SliderTimePicker),
            new FrameworkPropertyMetadata(TimeOnly.MinValue, OnMinTimeChanged)
        );

        public TimeOnly MinTime
        {
            get { return (TimeOnly)GetValue(MinTimeProperty); }
            set { SetValue(MinTimeProperty, value); }
        }


        public static readonly DependencyProperty MaxTimeProperty =
            DependencyProperty.Register(
                "EndTime",
                typeof(TimeOnly),
                typeof(SliderTimePicker),
                new FrameworkPropertyMetadata(TimeOnly.MaxValue, OnMaxTimeChanged)
            );

        public TimeOnly MaxTime
        {
            get { return (TimeOnly)GetValue(MaxTimeProperty); }
            set { SetValue(MaxTimeProperty, value); }
        }

        private static void OnMinTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SliderTimePicker timePicker)
            {
                timePicker.UpdateSliderLimits();
            }
        }

        private static void OnMaxTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SliderTimePicker timePicker)
            {
                timePicker.UpdateSliderLimits();
            }
        }

        public TimeOnly SelectedTime
        {
            get { return (TimeOnly)GetValue(SelectedTimeProperty); }
            set { SetValue(SelectedTimeProperty, value); }
        }

        private Slider? _slider;
        private TextBlock? _timeText;

        public event RoutedEventHandler? TimeChanged
        {
            add { AddHandler(TimeChangedEvent, value); }
            remove { RemoveHandler(TimeChangedEvent, value); }
        }

        private bool isDragging = false;

        static SliderTimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SliderTimePicker), new FrameworkPropertyMetadata(typeof(SliderTimePicker)));
        }

        private static void OnSelectedTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SliderTimePicker timePicker)
            {
                timePicker.UpdateSliderPosition();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _slider = GetTemplateChild("PART_Slider") as Slider;
            _timeText = GetTemplateChild("PART_TimeText") as TextBlock;

            if (_slider != null)
            {
                _slider.ValueChanged += SliderValueChanged;

                _slider.AddHandler(Thumb.DragStartedEvent, new DragStartedEventHandler((sender, e) =>
                {
                    isDragging = true;
                }));

                _slider.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler((sender, e) =>
                {
                    isDragging = false;
                    RaiseEvent(new RoutedEventArgs(TimeChangedEvent));
                }));

                UpdateSliderLimits();
                UpdateSliderPosition();
            }
            UpdateTimeText();


        }

        private void UpdateSliderLimits()
        {
            if (_slider == null)
            {
                return;
            }

            _slider.Minimum = MinTime.Second;
            _slider.Maximum = MaxTime.Second;
        }


        private void UpdateSliderPosition()
        {
            if (_slider == null)
            {
                return;
            }

            _slider.Value = SelectedTime.Second;
        }

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_slider == null)
                return;

            SelectedTime = TimeOnly.FromTimeSpan(TimeSpan.FromSeconds(_slider.Value));
            UpdateTimeText();

            if (!isDragging)
            {
                RaiseEvent(new RoutedEventArgs(TimeChangedEvent));
            }

            isDragging = false; //Reset the flag
        }

        private void UpdateTimeText()
        {
            if (_timeText != null)
            {
                _timeText.Text = SelectedTime.ToString("HH:mm");
            }
        }
    }
}