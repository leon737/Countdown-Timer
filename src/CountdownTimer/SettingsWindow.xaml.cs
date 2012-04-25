using System.Windows;

namespace CountdownTimer
{
    public partial class SettingsWindow : Window
    {
        private readonly SettingsModel settingsModel;

        public SettingsWindow(SettingsModel settingsModel)
        {
            this.settingsModel = settingsModel;
            DataContext = settingsModel;
            InitializeComponent();
        }

        private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            var startTimerCallback = settingsModel.StartTimerCallback;
            if (startTimerCallback != null)
                startTimerCallback();
        }
    }
}
