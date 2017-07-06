using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CountdownTimer
{
    public class RightClickButton : Button
    {
        public event RoutedEventHandler RightClick;

        private bool _clicked;

        public RightClickButton()
        {
            MouseRightButtonDown += RightClickButton_MouseRightButtonDown;
            MouseRightButtonUp += RightClickButton_MouseRightButtonUp;
        }

        protected void TriggerRightClickEvent(MouseButtonEventArgs e)
        {
            RightClick?.Invoke(this, e);
        }

        void RightClickButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsPressed = true;
            CaptureMouse();
            _clicked = true;
        }

        void RightClickButton_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();

            if (IsMouseOver && _clicked)
                RightClick?.Invoke(this, e);

            _clicked = false;
            IsPressed = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!IsMouseCaptured) return;

            bool isInside = false;

            VisualTreeHelper.HitTest(
                this,
                d =>
                {
                    if (d == this)
                    {
                        isInside = true;
                    }

                    return HitTestFilterBehavior.Stop;
                },
                ht => HitTestResultBehavior.Stop,
                new PointHitTestParameters(e.GetPosition(this)));

            IsPressed = isInside;
        }
    }
}
