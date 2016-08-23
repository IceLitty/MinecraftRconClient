using System;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MinecraftRconClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string connectFilePath = System.IO.Directory.GetCurrentDirectory() + @"\connect.ini";

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists(connectFilePath))
            {
                try
                {
                    string[] ip = INI.ReadIni("connect", "IP").Split('.');
                    inputIP1.Value = int.Parse(ip[0]);
                    inputIP2.Value = int.Parse(ip[1]);
                    inputIP3.Value = int.Parse(ip[2]);
                    inputIP4.Value = int.Parse(ip[3]);
                    inputPort.Value = int.Parse(INI.ReadIni("connect", "Port"));
                    inputPassword.Password = INI.ReadIni("connect", "Password");
                    Title += "|已读取: " + inputPassword.Password + "@" + inputIP1.Value.Value + "." + inputIP2.Value.Value + "." + inputIP3.Value.Value + "." + inputIP4.Value.Value + ":" + inputPort.Value.Value;
                    inputPassword.Focus();
                }
                catch (Exception)
                {
                    System.IO.File.Delete(connectFilePath);
                }
            }
        }

        private void MetroWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                Help help = new Help();
                this.Hide();
                help.ShowDialog();
                this.Show();
            }
        }

        private void inputIP1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                if (inputIP1.Value != null)
                {
                    string ip1 = inputIP1.Value.Value.ToString();
                    ip1.Replace(".", string.Empty);
                    inputIP1.Value = int.Parse(ip1);
                }
                inputIP2.Focus();
            }
        }

        private void inputIP2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                if (inputIP2.Value != null)
                {
                    string ip2 = inputIP2.Value.Value.ToString();
                    ip2.Replace(".", string.Empty);
                    inputIP2.Value = int.Parse(ip2);
                }
                inputIP3.Focus();
            }
        }

        private void inputIP3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                if (inputIP3.Value != null)
                {
                    string ip3 = inputIP3.Value.Value.ToString();
                    ip3.Replace(".", string.Empty);
                    inputIP3.Value = int.Parse(ip3);
                }
                inputIP4.Focus();
            }
        }

        private void inputIP4_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemSemicolon)
            {
                if (inputIP4.Value != null)
                {
                    string ip4 = inputIP4.Value.Value.ToString();
                    ip4.Replace(".", string.Empty);
                    inputIP4.Value = int.Parse(ip4);
                }
                inputPort.Focus();
            }
        }

        private void inputPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (inputPassword.Password == string.Empty)
                {
                    this.ShowMessageAsync("空密码警告", "", MessageDialogStyle.Affirmative, new MetroDialogSettings() { AffirmativeButtonText = "确认", NegativeButtonText = "取消" });
                }
                else
                {
                    this.Hide();
                    ConsoleWindow cw = new ConsoleWindow();
                    cw.connectRconServer(inputIP1.Value.Value + "." + inputIP2.Value.Value + "." + inputIP3.Value.Value + "." + inputIP4.Value.Value, int.Parse(inputPort.Value.Value.ToString()), inputPassword.Password);
                    cw.ShowDialog();
                    this.Close();
                }
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                INI.WriteIni("connect", "IP", inputIP1.Value.Value + "." + inputIP2.Value.Value + "." + inputIP3.Value.Value + "." + inputIP4.Value.Value);
                INI.WriteIni("connect", "Port", inputPort.Value.Value.ToString());
                INI.WriteIni("connect", "Password", inputPassword.Password);
                Title += "|已保存: " + inputPassword.Password + "@" + inputIP1.Value.Value + "." + inputIP2.Value.Value + "." + inputIP3.Value.Value + "." + inputIP4.Value.Value + ":" + inputPort.Value.Value;
            }
            catch (Exception)
            {
                this.ShowMessageAsync("保存失败", "", MessageDialogStyle.Affirmative, new MetroDialogSettings() { AffirmativeButtonText = "确认", NegativeButtonText = "取消" });
            }
        }

        private void helpBtn_Click(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            this.Hide();
            help.ShowDialog();
            this.Show();
        }
    }
}
