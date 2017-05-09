using System;
using System.ComponentModel;

namespace CountdownTimer
{
    public class SettingsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void StartTimer();
        public StartTimer StartTimerCallback { get; set; }

        private int _minutes;
        public int Minutes
        {
            get { return _minutes; }
            set
            {
                if (value != _minutes)
                {
                    _minutes = value;
                    OnPropertyChanged(nameof(Minutes));
                }
            }
        }

        private int _seconds;
        public int Seconds
        {
            get { return _seconds; }
            set
            {
                if (value != _seconds)
                {
                    _seconds = value;
                    OnPropertyChanged(nameof(Seconds));
                }
            }
        }

        private int _warmup;
        public int Warmup
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

        private TimeSpan _test;
        public TimeSpan Test
        {
            get { return _test; }
            set
            {
                if (value != _test)
                {
                    _test = value;
                    OnPropertyChanged(nameof(Test));
                }
            }
        }



        protected void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
