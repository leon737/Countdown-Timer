using System.Windows;

namespace CountdownTimer
{
    public partial class SettingsWindow : Window
    {
        private readonly SettingsModel _settingsModel;

        public SettingsWindow(SettingsModel settingsModel)
        {
            _settingsModel = settingsModel;
            DataContext = settingsModel;
            InitializeComponent();
        }

        private void OnStartButtonClick(object sender, RoutedEventArgs e) => _settingsModel.StartTimerCallback?.Invoke();
    }
}
