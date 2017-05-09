using System.Windows;
using System.Windows.Controls;

namespace CountdownTimer
{
    public partial class UpDown
    {
        static UpDown()
        {
            FrameworkPropertyMetadata valueMetadata = new FrameworkPropertyMetadata(1);
            ValueProperty = DependencyProperty.Register(nameof(Value), typeof(int), typeof(UpDown), valueMetadata);
            FrameworkPropertyMetadata stepMetadata = new FrameworkPropertyMetadata(1);
            StepProperty = DependencyProperty.Register(nameof(Step), typeof(int), typeof(UpDown), stepMetadata);
            FrameworkPropertyMetadata minValueMetadata = new FrameworkPropertyMetadata(0);
            MinValueProperty = DependencyProperty.Register(nameof(MinValue), typeof(int), typeof(UpDown), minValueMetadata);
            FrameworkPropertyMetadata maxValueMetadata = new FrameworkPropertyMetadata(10);
            MaxValueProperty = DependencyProperty.Register(nameof(MaxValue), typeof(int), typeof(UpDown), maxValueMetadata);
        }

        public UpDown()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty StepProperty;
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                int numValue = value;
                if (numValue < MinValue) numValue = MinValue;
                if (numValue > MaxValue) numValue = MaxValue;
                SetValue(ValueProperty, numValue);
            }
        }

        public int Step
        {
            get { return (int)GetValue(StepProperty); }
            set
            {
                SetValue(StepProperty, value);
            }
        }

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set
            {
                SetValue(MinValueProperty, value);
            }
        }

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set
            {
                SetValue(MaxValueProperty, value);
            }
        }

        private void CmdUpClick(object sender, RoutedEventArgs e)
        {
            Value += Step;
        }

        private void CmdDownClick(object sender, RoutedEventArgs e)
        {
            Value -= Step;
        }

        private void TxtNumTextChanged(object sender, TextChangedEventArgs e)
        {
            int numValue;
            if (int.TryParse(txtNum.Text, out numValue))
                Value = numValue;
        }

    }
}
