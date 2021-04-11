using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ChocolaTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Timer_Tick(this, EventArgs.Empty);
            Timer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            Timer.Tick += Timer_Tick;
            // Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var remainderSeconds = Seconds % 60;
            var totalMinutes = Seconds / 60;
            var remainderMinutes = totalMinutes % 60;
            var totalHours = Seconds / 3600;
            var map = new Dictionary<string, int>
            {
                { "ts", Seconds },
                { "rs", remainderSeconds },
                { "tm", totalMinutes },
                { "rm", remainderMinutes },
                { "th", totalHours }
            };
            var text = FormatString;
            foreach (var key in map.Keys)
            {
                text = text.Replace(key, map[key].ToString("D2"));
            }
            this.TimeTextBlock.Text = text;
            this.Seconds += Delta;
            // 不允许负数
            if(this.Seconds < 0)
            {
                this.Seconds = -this.Seconds;
                Delta = -Delta;
            }
        }

        public DispatcherTimer Timer;
        public int Seconds = 0;
        public int Delta = 1;
        public string FormatString = "tm:rs";
        public int TextFontSize = 100;

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Timer?.Stop();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            TextFontSize += e.Delta * 4 / 120;
            if(TextFontSize < 24)
            {
                TextFontSize = 24;
            }
            this.TimeTextBlock.FontSize = TextFontSize;
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Timer.IsEnabled)
            {
                Timer.Stop();
                this.TimeTextBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 104, 104, 104));
            }
            else
            {
                Timer.Start();
                this.TimeTextBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 215, 0));
            }
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            this.Seconds = 0;
            this.Delta = 1;
        }

        private void Revert(object sender, RoutedEventArgs e)
        {
            this.Delta = -this.Delta;
        }

        private void SetTime(object sender, RoutedEventArgs e)
        {
            var window = new InputTimeWindow(x =>
            {
                this.Seconds = x;
                this.Delta = -1;
                Timer_Tick(this, EventArgs.Empty);
            });
            window.Owner = this;
            window.Show();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
