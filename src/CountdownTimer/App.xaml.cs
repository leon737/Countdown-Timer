using System;
using System.Windows;

namespace CountdownTimer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        readonly SettingsModel _settingsModel = new SettingsModel();
        DisableScreensaver _disableScreensaver = DisableScreensaver.Instance;

        protected override void OnStartup(StartupEventArgs e)
        {
            SetDefaultSettings();
            var main = new MainWindow(_settingsModel);
            main.Show();
            var settings = new SettingsWindow(_settingsModel);
            settings.Show();
        }

        private void SetDefaultSettings()
        {
            _settingsModel.Minutes = 4;
            _settingsModel.Seconds = 0;
            _settingsModel.Warmup = 20;
            _settingsModel.Test = new TimeSpan(0, 3, 2);
        }
    }
}
