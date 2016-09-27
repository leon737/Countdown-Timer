using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace CountdownTimer
{
    public class DisableScreensaver
    {
        [DllImport("user32")]
        private static extern void keybd_event(byte bVirtualKey, byte bScanCode, int dwFlags, int dwExtraInfo);

        private const byte VK_LSHIFT = 0xA0;
        private const int KEYEVENTF_KEYUP = 0x0002;

        private Timer _timer;

        private DisableScreensaver()
        {
            _timer = new Timer(SendKey, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        public static DisableScreensaver Instance { get; } = new DisableScreensaver();


        private void SendKey(object sender) => keybd_event(VK_LSHIFT, 0x45, KEYEVENTF_KEYUP, 0);
    }
}
