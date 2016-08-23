using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace MinecraftRconClient
{
    /// <summary>
    /// Help.xaml 的交互逻辑
    /// </summary>
    public partial class Help : MetroWindow
    {
        public Help()
        {
            InitializeComponent();
        }

        private void web_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.mcbbs.net/thread-625007-1-1.html");
        }
    }
}
