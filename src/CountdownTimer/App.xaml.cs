using System;
using System.Windows;
using CountdownTimer.Models;

namespace CountdownTimer
{
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
            _settingsModel.Time = TimeSpan.FromMinutes(4);
            _settingsModel.Warmup = TimeSpan.FromSeconds(20);
        }
    }
}
