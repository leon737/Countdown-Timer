﻿using System;
using System.Windows;
using CountdownTimer.Models;

namespace CountdownTimer
{
    public partial class SettingsWindow : Window
    {

        public SettingsWindow(SettingsModel settingsModel)
        {
            DataContext = settingsModel;
            InitializeComponent();
            Closed += OnClose;
        }

        private void OnClose(object sender, EventArgs e)
        {
            WindowManager.Instance.Release(this);
        }
    }
}
