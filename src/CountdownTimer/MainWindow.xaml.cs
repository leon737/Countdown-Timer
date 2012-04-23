using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;

namespace CountdownTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
                
        public MainWindow()
        {
            InitializeComponent();
            sw = new Stopwatch();
            sw.Start();
            Task task = Task.Factory.StartNew(() =>
            {
                while (sw.ElapsedMilliseconds / 1000 <= secondsToGo)
                {
                    if (PropertyChanged != null)
                    {
                        if (!IsWarmup && !warmUpSignaled)
                        {
                            warmUpSignaled = true;
                            PropertyChanged(this, new PropertyChangedEventArgs("DesiredColor"));                            
                        }
                        PropertyChanged(this, new PropertyChangedEventArgs("TimeLeft"));                                                
                    }
                    Thread.Sleep(1000);
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DesiredColor"));
                }
            });
        }


        bool warmUpSignaled = false;
        int warmUpSeconds = 20;
        int secondsToGo = 5 * 60 + 20;
        Stopwatch sw;

        public Brush DesiredColor
        {
            get
            {
                return new SolidColorBrush(GetDesiredColor());
            }
        }

        Color GetDesiredColor()
        {
            if (!IsRunning)
                return Colors.Red;
            if (IsWarmup)
                return Colors.Yellow;
            return Colors.Green;
        }

        public bool IsRunning
        {
            get
            {
                int secondsLeft = secondsToGo - (int)(sw.ElapsedMilliseconds / 1000);
                return secondsLeft > 0;
            }
        }

        public bool IsWarmup
        {
            get
            {
                return (int)(sw.ElapsedMilliseconds / 1000) < warmUpSeconds;
            }
        }     


        public string TimeLeft
        {
            get
            {
                int secondsLeft = secondsToGo - (int)(sw.ElapsedMilliseconds / 1000);
                int minutes = secondsLeft / 60;
                int seconds = secondsLeft % 60;
                return string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
