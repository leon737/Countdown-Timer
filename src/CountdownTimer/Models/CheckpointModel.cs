using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CountdownTimer.Models
{
    public class CheckpointModel : INotifyPropertyChanged
    {

        private TimeSpan _time;

        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}