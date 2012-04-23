using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace CountdownTimer
{
    public partial class UpDown
    {
        public UpDown()
        {
            InitializeComponent();
            txtNum.Text = numValue.ToString(CultureInfo.InvariantCulture);
        }

        private int numValue;
        public int NumValue
        {
            get { return numValue; }
            set
            {
                numValue = value;
                if (numValue < MinValue) numValue = MinValue;
                if (numValue > MaxValue) numValue = MaxValue;
                txtNum.Text = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public int Step { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        private void CmdUpClick(object sender, RoutedEventArgs e)
        {
            numValue += Step;
        }

        private void CmdDownClick(object sender, RoutedEventArgs e)
        {
            NumValue-=Step;
        }

        private void TxtNumTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(txtNum.Text, out numValue))
                txtNum.Text = numValue.ToString(CultureInfo.InvariantCulture);
        }

    }
}
