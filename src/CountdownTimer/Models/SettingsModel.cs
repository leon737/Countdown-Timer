using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace CountdownTimer.Models
{
    public class SettingsModel : INotifyPropertyChanged
    {

        private const int MaxCheckpointsCount = 4;

        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void StartTimer();
        public StartTimer StartTimerCallback { get; set; }
        public ICommand AddCheckpointCommand { get; set; }
        public ICommand RemoveCheckpointCommand { get; set; }
        public ICommand StartTimerCommand { get; set; }

        
        public SettingsModel()
        {
            AddCheckpointCommand = new RelayCommand(AddCheckpoint, _=> true);
            RemoveCheckpointCommand = new RelayCommand(RemoveCheckpoint, _ => true);
            StartTimerCommand = new RelayCommand(StartTimerCommandCb,  _ => true);
        }

        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                if (value != _time)
                {
                    _time = value;
                    OnPropertyChanged(nameof(Time));
                }
            }
        }

        private TimeSpan _warmup;
        public TimeSpan Warmup
        {
            get { return _warmup; }
            set
            {
                if (value != _warmup)
                {
                    _warmup = value;
                    OnPropertyChanged(nameof(Warmup));
                }
            }
        }

        public ObservableCollection<CheckpointModel> Checkpoints { get; } = new ObservableCollection<CheckpointModel>();

        private void AddCheckpoint(object obj)
        {
            Checkpoints.Add(new CheckpointModel());
            OnPropertyChanged(nameof(Checkpoints));
            OnPropertyChanged(nameof(CanAddCheckpoints));
        }

        private void RemoveCheckpoint(object obj)
        {
            Checkpoints.Remove((CheckpointModel)obj);
            OnPropertyChanged(nameof(Checkpoints));
            OnPropertyChanged(nameof(CanAddCheckpoints));
        }

        private void StartTimerCommandCb(object obj) => StartTimerCallback?.Invoke();


        protected void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public bool CanAddCheckpoints => Checkpoints.Count() < MaxCheckpointsCount;
    }
}
