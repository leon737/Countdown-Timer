using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace CountdownTimer
{
    public class WindowManager
    {
        private readonly IList<Window> _windows = new List<Window>();

        private WindowManager() { }

        public static WindowManager Instance { get; } = new WindowManager();

        public void RegisterWindow(Window window) => _windows.Add(window);

        public void Release(Window source)
        {
            foreach (var window in _windows.Where(x => x != null && !ReferenceEquals(x,source)))
                window.Close();
            _windows.Clear();
        }

    }
}
