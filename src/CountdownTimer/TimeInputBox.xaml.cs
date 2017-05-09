using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CountdownTimer
{
    public partial class TimeInputBox : INotifyPropertyChanged
    {

        static TimeInputBox()
        {
            FrameworkPropertyMetadata valueMetadata = new FrameworkPropertyMetadata();
            ValueProperty = DependencyProperty.Register(nameof(Value), typeof(TimeSpan), typeof(TimeInputBox), valueMetadata);
            FrameworkPropertyMetadata stepMetadata = new FrameworkPropertyMetadata(new TimeSpan(0, 1, 15));
            StepProperty = DependencyProperty.Register(nameof(Step), typeof(TimeSpan), typeof(TimeInputBox), stepMetadata);
            FrameworkPropertyMetadata minValueMetadata = new FrameworkPropertyMetadata(new TimeSpan());
            MinValueProperty = DependencyProperty.Register(nameof(MinValue), typeof(TimeSpan), typeof(TimeInputBox), minValueMetadata);
            FrameworkPropertyMetadata maxValueMetadata = new FrameworkPropertyMetadata(TimeSpan.FromMinutes(10));
            MaxValueProperty = DependencyProperty.Register(nameof(MaxValue), typeof(TimeSpan), typeof(TimeInputBox), maxValueMetadata);
        }

        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty StepProperty;
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;

        public TimeInputBox()
        {
            InitializeComponent();
        }


        public TimeSpan Value
        {
            get { return (TimeSpan)GetValue(ValueProperty); }
            set
            {
                TimeSpan numValue = value;
                if (numValue < MinValue) numValue = MinValue;
                if (numValue > MaxValue) numValue = MaxValue;
                SetValue(ValueProperty, numValue);
                OnPropertyChanged(nameof(ValueMins));
                OnPropertyChanged(nameof(ValueSecs));
            }
        }

        public int ValueMins
        {
            get { return Value.Minutes; }
            set
            {
                var prevValue = Value.Minutes;
                if (prevValue != value)
                {
                    Value = new TimeSpan(0, value, Value.Seconds);
                    OnPropertyChanged();
                }
            }
        }

        public int ValueSecs
        {
            get { return Value.Seconds; }
            set
            {
                var prevValue = ValueSecs;
                if (prevValue != value)
                {
                    Value = new TimeSpan(0, Value.Minutes, value);
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan Step
        {
            get { return (TimeSpan)GetValue(StepProperty); }
            set
            {
                SetValue(StepProperty, value);
                OnPropertyChanged(nameof(StepMins));
                OnPropertyChanged(nameof(StepSecs));
            }
        }

        public int StepMins => Step.Minutes;

        public int StepSecs => Step.Seconds;

        public TimeSpan MinValue
        {
            get { return (TimeSpan)GetValue(MinValueProperty); }
            set
            {
                SetValue(MinValueProperty, value);
                OnPropertyChanged(nameof(MinValueMins));
                OnPropertyChanged(nameof(MinValueSecs));
            }
        }

        public int MinValueMins => MinValue.Minutes;

        public int MinValueSecs => 0;

        public TimeSpan MaxValue
        {
            get { return (TimeSpan)GetValue(MaxValueProperty); }
            set
            {
                SetValue(MaxValueProperty, value);
                OnPropertyChanged(nameof(MaxValueMins));
                OnPropertyChanged(nameof(MaxValueSecs));
            }
        }

        public int MaxValueMins => MaxValue.Minutes;

        public int MaxValueSecs => MaxValue.Minutes > 1 ? 60 : MaxValue.Seconds;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
