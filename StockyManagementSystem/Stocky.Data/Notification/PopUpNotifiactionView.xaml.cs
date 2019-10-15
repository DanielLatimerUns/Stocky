using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;


namespace Stocky.Utility
{
    /// <summary>
    /// Interaction logic for PopUpNotifiactionView.xaml
    /// </summary>
    public partial class PopUpNotificationView : Window
    {
        private DispatcherTimer AutoClose;
        public PopUpNotificationView()
        {
            InitializeComponent();
            StartTimer();
            
        }

        private void StartTimer()
        {
            AutoClose = new DispatcherTimer();
            AutoClose.Interval = TimeSpan.FromSeconds(5);
            AutoClose.Tick += Tick;
            AutoClose.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= Tick;

            Storyboard s = FindResource("CloseAnimation") as Storyboard;
            Storyboard.SetTarget(s, this.window);
            s.Begin();
            s.Completed += S_Completed;
            
        }

        private void S_Completed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var workingarea = System.Windows.SystemParameters.WorkArea;
            this.Left = workingarea.Right - this.Width - 5;
            this.Top = workingarea.Bottom - this.Height - 5;

            this.Topmost = true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
            this.Activate();
        }

        private void window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AutoClose.Stop();
            Close();
        }

    }
}
