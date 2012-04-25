using System.Windows;
using System.Windows.Media;
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
        private readonly SettingsModel settingsModel;
        bool warmUpSignaled = false;
        int warmUpSeconds = 20;
        int secondsToGo = 5 * 60 + 20;
        Stopwatch sw;

        public MainWindow(SettingsModel settingsModel)
        {
            this.settingsModel = settingsModel;
            settingsModel.StartTimerCallback += StartTimer;
            sw = new Stopwatch();
            InitializeComponent();            
        }


        public void StartTimer()
        {
            warmUpSignaled = false;
            warmUpSeconds = settingsModel.Warmup;
            secondsToGo = settingsModel.Minutes * 60  + settingsModel.Seconds + warmUpSeconds;
            RunTimer();
        }

        private void RunTimer()
        {
            sw = new Stopwatch();
            sw.Start();
            PropertyChanged(this, new PropertyChangedEventArgs("DesiredColor"));
            PropertyChanged(this, new PropertyChangedEventArgs("TimeLeft"));
            Task.Factory.StartNew(() =>
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
