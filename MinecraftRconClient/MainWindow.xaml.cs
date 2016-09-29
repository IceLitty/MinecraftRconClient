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
        private bool sameAsFile = false;
        private int ip0 = 0;
        private int ip1 = 0;
        private int ip2 = 0;
        private int ip3 = 0;
        private int port = 0;
        private string passwd = "passwd";

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists(connectFilePath))
            {
                try
                {
                    loadFile();
                    Title = "登陆|已读取: " + inputPassword.Password + "@" + inputIP1.Value.Value + "." + inputIP2.Value.Value + "." + inputIP3.Value.Value + "." + inputIP4.Value.Value + ":" + inputPort.Value.Value;
                    this.ToolTip = Title;
                    sameAsFile = true;
                    checkConnectChanged();
                    inputPassword.Focus();
                }
                catch (Exception)
                {
                    System.IO.File.Delete(connectFilePath);
                }
            }
        }

        private void loadFile()
        {
            string[] ip = INI.ReadIni("connect", "IP").Split('.');
            ip0 = int.Parse(ip[0]);
            ip1 = int.Parse(ip[1]);
            ip2 = int.Parse(ip[2]);
            ip3 = int.Parse(ip[3]);
            port = int.Parse(INI.ReadIni("connect", "Port"));
            passwd = INI.ReadIni("connect", "Password");
            inputIP1.Value = ip0;
            inputIP2.Value = ip1;
            inputIP3.Value = ip2;
            inputIP4.Value = ip3;
            inputPort.Value = port;
            inputPassword.Password = passwd;
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
            else if (e.Key == Key.Escape)
            {
                this.Close();
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
                inputIP2.SelectAll();
            }
        }

        private void inputIP1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            checkContentChanged("ip0");
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
                inputIP3.SelectAll();
            }
        }

        private void inputIP2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            checkContentChanged("ip1");
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
                inputIP4.SelectAll();
            }
        }

        private void inputIP3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            checkContentChanged("ip2");
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
                inputPort.SelectAll();
            }
        }

        private void inputIP4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            checkContentChanged("ip3");
        }

        private void inputPort_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            checkContentChanged("port");
        }

        private void inputPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                login();
            }
        }

        private void inputPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            checkContentChanged("passwd");
        }

        private void login()
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

        private void checkConnectChanged()
        {
            if (sameAsFile)
            {
                saveBtn.Content = "登陆";
            }
            else
            {
                saveBtn.Content = "保存";
            }
        }

        private void checkContentChanged(string content)
        {
            //MessageBox.Show(inputIP1.Value + "|" + ip0 + "\r" + inputIP2.Value + "|" + ip1 + "\r" + inputIP3.Value + "|" + ip2 + "\r" + inputIP4.Value + "|" + ip3 + "\r" + inputPort.Value + "|" + port + "\r" + inputPassword.Password + "|" + passwd);
            switch (content)
            {
                case "ip0":
                    if (inputIP1.Value.Value != ip0) { sameAsFile = false; checkConnectChanged(); } else { sameAsFile = true; checkConnectChanged(); }
                    break;
                case "ip1":
                    if (inputIP2.Value.Value != ip1) { sameAsFile = false; checkConnectChanged(); } else { sameAsFile = true; checkConnectChanged(); }
                    break;
                case "ip2":
                    if (inputIP3.Value.Value != ip2) { sameAsFile = false; checkConnectChanged(); } else { sameAsFile = true; checkConnectChanged(); }
                    break;
                case "ip3":
                    if (inputIP4.Value.Value != ip3) { sameAsFile = false; checkConnectChanged(); } else { sameAsFile = true; checkConnectChanged(); }
                    break;
                case "port":
                    if (inputPort.Value.Value != port) { sameAsFile = false; checkConnectChanged(); } else { sameAsFile = true; checkConnectChanged(); }
                    break;
                case "passwd":
                    if (inputPassword.Password != passwd) { sameAsFile = false; checkConnectChanged(); } else { sameAsFile = true; checkConnectChanged(); }
                    break;
                default:
                    break;
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sameAsFile)
            {
                login();
            }
            else
            {
                try
                {
                    INI.WriteIni("connect", "IP", inputIP1.Value.Value + "." + inputIP2.Value.Value + "." + inputIP3.Value.Value + "." + inputIP4.Value.Value);
                    INI.WriteIni("connect", "Port", inputPort.Value.Value.ToString());
                    INI.WriteIni("connect", "Password", inputPassword.Password);
                    Title = "登陆|已保存: " + inputPassword.Password + "@" + inputIP1.Value.Value + "." + inputIP2.Value.Value + "." + inputIP3.Value.Value + "." + inputIP4.Value.Value + ":" + inputPort.Value.Value;
                    this.ToolTip = Title;
                    loadFile();
                }
                catch (Exception)
                {
                    this.ShowMessageAsync("保存失败", "", MessageDialogStyle.Affirmative, new MetroDialogSettings() { AffirmativeButtonText = "确认", NegativeButtonText = "取消" });
                }
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
