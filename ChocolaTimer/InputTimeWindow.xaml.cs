using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChocolaTimer
{
    /// <summary>
    /// InputTimeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InputTimeWindow : Window
    {
        public InputTimeWindow(Action<int> action)
        {
            InitializeComponent();
            this.Action = action;
            this.SecondTextBox.Focus();
        }

        private Action<int> Action;

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                int.TryParse(SecondTextBox.Text, out var second);
                int.TryParse(MinuteTextBox.Text, out var minute);
                int.TryParse(HourTextBox.Text, out var hour);
                var value = second + minute * 60 + hour * 3600;
                this.Action.Invoke(value);
                this.Close();
            }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
