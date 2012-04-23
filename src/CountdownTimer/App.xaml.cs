using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace CountdownTimer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        SettingsModel settingsModel = new SettingsModel();        

        protected override void OnStartup(StartupEventArgs e)
        {
            SetDefaultSettings();
            var main = new MainWindow();
            main.Show();
            var settings = new SettingsWindow(settingsModel);
            settings.Show();
        }

        private void SetDefaultSettings()
        {
            settingsModel.Minutes = 4;
            settingsModel.Seconds = 0;
            settingsModel.Warmup = 20;
        }
    }
}
