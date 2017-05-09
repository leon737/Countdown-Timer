using System;
using System.Windows;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.Media;
using CountdownTimer.Models;
using Functional.Fluent.Pattern;

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
        private Guid _runningStopwatch = Guid.Empty;

        Lazy<Matcher<State, Color>> _desiredColorMatcher = new Lazy<Matcher<State, Color>>(() => new Matcher<State, Color>()
            .With(State.Running, Colors.Green)
            .With(State.Warmup, Colors.Yellow)
            .Else(Colors.Red));

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
            _runningStopwatch = Guid.NewGuid();
            _sw.Start();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DesiredColor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeLeft)));
            Task.Factory.StartNew(() =>
            {
                var uid = _runningStopwatch;
                while (_sw.ElapsedMilliseconds / 1000 <= _secondsToGo)
                {
                    if (_runningStopwatch != uid)
                        return;

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
        
        Color GetDesiredColor() => _desiredColorMatcher.Value.Match(CurrentState);

        public bool IsRunning => _secondsToGo - (int)(_sw.ElapsedMilliseconds / 1000) > 0;

        public bool IsWarmup => (int)(_sw.ElapsedMilliseconds / 1000) < _warmUpSeconds;

        public State CurrentState => IsWarmup ? State.Warmup : IsRunning ? State.Running : State.Off;


        public string TimeLeft
        {
            get
            {
                int secondsLeft = _secondsToGo - (int)(_sw.ElapsedMilliseconds / 1000);
                int minutes = secondsLeft / 60;
                int seconds = secondsLeft % 60;
                if (minutes < 0) minutes = 0;
                if (seconds < 0) seconds = 0;
                return $"{minutes:00}:{seconds:00}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
