using System.Windows;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.Media;

namespace CountdownTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private const string SoundFilePath = "Resources/sound.wav";
        private readonly SettingsModel _settingsModel;
        bool _warmUpSignaled = false;
        int _warmUpSeconds = 20;
        int _secondsToGo = 5 * 60 + 20;
        private Stopwatch _sw;

        public MainWindow(SettingsModel settingsModel)
        {
            this._settingsModel = settingsModel;
            settingsModel.StartTimerCallback += StartTimer;
            _sw = new Stopwatch();
            InitializeComponent();
        }


        public void StartTimer()
        {
            _warmUpSignaled = false;
            _warmUpSeconds = _settingsModel.Warmup;
            _secondsToGo = _settingsModel.Minutes * 60 + _settingsModel.Seconds + _warmUpSeconds;
            RunTimer();
        }

        private void RunTimer()
        {
            _sw = new Stopwatch();
            _sw.Start();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DesiredColor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeLeft)));
            Task.Factory.StartNew(() =>
                                  {
                                      while (_sw.ElapsedMilliseconds / 1000 <= _secondsToGo)
                                      {
                                          if (PropertyChanged != null)
                                          {
                                              if (!IsWarmup && !_warmUpSignaled)
                                              {
                                                  _warmUpSignaled = true;
                                                  PlaySoundAsync(2);
                                                  PropertyChanged(this, new PropertyChangedEventArgs(nameof(DesiredColor)));
                                              }
                                              PropertyChanged(this, new PropertyChangedEventArgs(nameof(TimeLeft)));
                                          }
                                          Thread.Sleep(1000);
                                      }
                                      if (PropertyChanged != null)
                                      {
                                          PlaySoundAsync(3);
                                          PropertyChanged(this, new PropertyChangedEventArgs(nameof(DesiredColor)));
                                      }
                                  });
        }

        private void PlaySoundAsync(int count)
        {
            Task.Factory.StartNew(() =>
            {
                var player = new SoundPlayer(SoundFilePath);
                player.Load();
                for (var i = 0; i < count; ++i)
                    player.PlaySync();
            });
        }

        public Brush DesiredColor => new SolidColorBrush(GetDesiredColor());

        Color GetDesiredColor() => !IsRunning ? Colors.Red : (IsWarmup ? Colors.Yellow : Colors.Green);

        public bool IsRunning => _secondsToGo - (int)(_sw.ElapsedMilliseconds / 1000) > 0;

        public bool IsWarmup => (int)(_sw.ElapsedMilliseconds / 1000) < _warmUpSeconds;


        public string TimeLeft
        {
            get
            {
                int secondsLeft = _secondsToGo - (int)(_sw.ElapsedMilliseconds / 1000);
                int minutes = secondsLeft / 60;
                int seconds = secondsLeft % 60;
                return $"{minutes:00}:{seconds:00}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
