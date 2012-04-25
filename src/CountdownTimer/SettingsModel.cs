using System.ComponentModel;

namespace CountdownTimer
{
    public class SettingsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void StartTimer();
        public StartTimer StartTimerCallback { get; set; }

        private int minutes;
        public int Minutes
        {
            get { return minutes; }
            set
            {
                if (value != minutes)
                {
                    minutes = value;
                    OnPropertyChanged("Minutes");
                }
            }
        }

        private int seconds;
        public int Seconds
        {
            get { return seconds; }
            set
            {
                if (value != seconds)
                {
                    seconds = value;
                    OnPropertyChanged("Seconds");
                }
            }
        }

        private int warmup;
        public int Warmup
        {
            get { return warmup; }
            set
            {
                if (value != warmup)
                {
                    warmup = value;
                    OnPropertyChanged("Warmup");
                }
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            var v = PropertyChanged;
            if (v != null)
                v(this, new PropertyChangedEventArgs(propName));
        }
    }
}
