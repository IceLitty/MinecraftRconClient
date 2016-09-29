using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;
using MinecraftServerRCON;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading;
using System.Windows.Threading;

namespace MinecraftRconClient
{
    /// <summary>
    /// ConsoleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConsoleWindow : MetroWindow
    {
        public ConsoleWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            readLog();
            readHistory();
            historyIndex = history.Count();
            commandIn.Focus();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (tellMainThread != string.Empty)
            {
                setLog(tellMainThread);
                readLog();
                tellMainThread = string.Empty;
            }
            if (ESCPassTime >= 0)
            {
                ESCPassTime--;
            }
        }

        private int ESCPassTime = 0;
        private string tellMainThread = string.Empty;
        private RCONClient rcon = RCONClient.INSTANCE;

        private void MetroWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                consoleOut.Height = this.Height - 80;
                commandIn.Width = this.Width - 20;
            } catch (Exception) { }
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            data.Add("§8 - Continued on " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - ");
            setLog();
            setHistory();
        }

        public void connectRconServer(string ip, int port, string passwd)
        {
            rcon.setupStream(ip, port: port, password: passwd);
            Title += "|连接至: " + ip + ":" + port;
        }

        private string getSTDOUT(string cmd)
        {
            return rcon.sendMessage(RCONMessageType.Command, cmd);
        }

        private void commandIn_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //给判据做准备
            bool cmdEqu_name = false;
            if (commandIn.Text.Length > 5)
            {
                if (commandIn.Text.Substring(commandIn.Text.Length - 5, 5) == "name=" || commandIn.Text.Substring(commandIn.Text.Length - 6, 6) == "name=!") cmdEqu_name = true;
            }
            //执行指令
            if (e.Key == Key.Enter)
            {
                if (commandIn.Text.Length > 0)
                {
                    if (commandIn.Text.Substring(0, 1) == "`")
                    {
                        //软件内置命令
                        string cmd = commandIn.Text.Substring(1, commandIn.Text.Length - 1);
                        if (cmd == "clear" || cmd == "cls") //删除log和history文件，清屏
                        {
                            File.Delete(Directory.GetCurrentDirectory() + @"\log.ini");
                            data = new List<string>();
                            File.Delete(Directory.GetCurrentDirectory() + @"\history.ini");
                            history = new List<string>();
                            historyIndex = 0;
                            readLog();
                            readHistory();
                        }
                        else if (cmd.Length >= 4 && cmd.Substring(0, 4) == "echo")
                        {
                            setLog("echo §e>");
                            string tmp = cmd.Substring(4, cmd.Length - 4);
                            if (tmp.Length > 1)
                            {
                                tmp = tmp.Substring(1, tmp.Length - 1).Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\t", "\t");
                            }
                            else
                            {
                                GetComputerInfo getinfo = new GetComputerInfo();
                                if (c_getSystemInfo[0] == null)
                                {
                                    setLog("Initializing...");
                                    try
                                    {
                                        Thread t = new Thread(() =>
                                        {
                                            c_getSystemInfo = getinfo.GetInfo();
                                            tellMainThread = "Get system info finished! Please type command again.";
                                        });
                                        t.Start();
                                    }
                                    catch (Exception) { }
                                }
                                else
                                {
                                    c_getSystemInfo[1] = getinfo.fixTime();
                                }
                                tmp = c_getSystemInfo[0] + c_getSystemInfo[1] + c_getSystemInfo[2];
                            }
                            setLog(tmp);
                            readLog();
                        }
                        else if (cmd == "help" || cmd == "?")//内建指令帮助文档
                        {
                            setLog("======================= §l§b帮助§r ========================");
                            setLog("§7在下列指令前输入`符号以表示此指令是内建指令。");
                            setLog("§6clear  \t§e> §r可清除日志和历史文件并清空终端输出。可用§6cls§r代替。");
                            setLog("§6echo   \t§e> §r回显用户输入的文本，如无文本则回显测试文本。测试文本第一次使用时需初始化，未加载好仅会显示空回显文本。");
                            setLog("§6help   \t§e> §r获取本帮助文档。可用§6?§r代替。");
                            setLog("§6restart\t§e> §r重新启动应用程序。");
                            setLog("§6stop   \t§e> §r退出程序。可用§6exit§r代替。");
                            setLog("==================== §bTab§l§b补全功能§r ====================");
                            setLog("§7下列帮助是操作说明：Tab按键补全功能，按空格键确认选择。");
                            setLog("§a      \t§b> §r在不输入任何文本时，按tab键可以补全全部指令，第一次使用需初始化。");
                            setLog("§a      \t§b> §r同样，在仅输入单个字母或两个字母时可使用tab键继续补全，如果初始化过则不需要再向服务器进行查询。");
                            setLog("§aban   \t§b> §r在输入此指令和空格后，按tab键可以切换玩家列表的选择，同时还可用于§aban§r、§agive§r和§atellraw§r指令。");
                            setLog("§a      \t§b> §r在输入任何指令中，包含“name=”或“name=!”即可按tab补全玩家名，实体名称暂不支持。");
                            readLog();
                        }
                        else if (cmd == "restart")//重启
                        {
                            rcon.Dispose();
                            System.Diagnostics.Process.Start(System.Reflection.Assembly.GetEntryAssembly().Location);
                            this.Close();
                        }
                        else if (cmd == "stop" || cmd == "exit")//退出
                        {
                            rcon.Dispose();
                            this.Close();
                        }
                        else
                        {
                            setLog("§l未发现此内建指令，请检查拼写是否正确！");
                            readLog();
                        }
                        setHistory(commandIn.Text);
                        readHistory();
                        historyIndex = history.Count();
                    }
                    else
                    {
                        string nowReturn = getSTDOUT(commandIn.Text);
                        setHistory(commandIn.Text);
                        setLog(nowReturn);
                        readLog();
                        readHistory();
                        historyIndex = history.Count();
                    }
                    commandIn.Text = string.Empty;
                }
            }
            else if (e.Key == Key.Escape)
            {
                ESCPassTime = ESCPassTime + 10;
                if (ESCPassTime > 10)  //一秒内按2次esc退出程序
                {
                    this.Close();
                }
                else
                {
                    setLog("Press again to exit program!");
                    readLog();
                }
            }
            else if (e.Key == Key.Up)   //show history
            {
                if (historyIndex > 0)
                {
                    historyIndex--;
                    commandIn.Text = history[historyIndex];
                    commandIn.ToolTip = "历史纪录第" + (historyIndex + 1) + "/" + history.Count() + "条";
                }
                commandIn.Select(commandIn.Text.Length, 0);
                commandIn.ScrollToEnd();
            }
            else if (e.Key == Key.Down) //show history
            {
                if (historyIndex < history.Count() - 1)
                {
                    historyIndex++;
                    commandIn.Text = history[historyIndex];
                    commandIn.ToolTip = "历史纪录第" + (historyIndex + 1) + "/" + history.Count() + "条";
                }
                else if (historyIndex < history.Count())
                {
                    historyIndex++;
                    commandIn.Text = string.Empty;
                    commandIn.ToolTip = null;
                }
                commandIn.Select(commandIn.Text.Length, 0);
                commandIn.ScrollToEnd();
            }
            else if (e.Key == Key.Space)//确认补全
            {
                if (c_help.Count != 0 && commandIn.Text.Length == 0)
                {
                    int tempindex = 0;
                    if (c_help.Count() > 1 && c_help_index != 0)
                    {
                        tempindex = c_help_index - 1;
                    }
                    if (c_help.Count() != 0)
                    {
                        e.Handled = true;
                        tempindex = c_help.Count() - 1;
                        commandIn.Text = c_help[tempindex].RemoveColorCodes() + " ";
                        commandIn.Select(commandIn.Text.Length, 0);
                        commandIn.ScrollToEnd();
                    }
                }
                else if (commandIn.Text.Length == 1 && commandIn.Text != " ")
                {
                    int tempindex = 0;
                    if (c_help_search.Count() > 1 && c_help_search_index != 0)
                    {
                        tempindex = c_help_search_index - 1;
                    }
                    if(c_help_search.Count() != 0)
                    {
                        e.Handled = true;
                        tempindex = c_help_search.Count() - 1;
                        commandIn.Text = c_help_search[tempindex].RemoveColorCodes() + " ";
                        commandIn.Select(commandIn.Text.Length, 0);
                        commandIn.ScrollToEnd();
                    }
                }
                else if (commandIn.Text.Length == 2 && commandIn.Text != " ")
                {
                    int tempindex = 0;
                    if (c_help_search.Count() > 1 && c_help_search_index != 0)
                    {
                        tempindex = c_help_search_index - 1;
                    }
                    if (c_help_search.Count() != 0)
                    {
                        e.Handled = true;
                        tempindex = c_help_search.Count() - 1;
                        commandIn.Text = c_help_search[tempindex].RemoveColorCodes() + " ";
                        commandIn.Select(commandIn.Text.Length, 0);
                        commandIn.ScrollToEnd();
                    }
                }
                else if (c_playerList.Count() != 0 && (commandIn.Text == "ban " || commandIn.Text == "op " || commandIn.Text == "give " || commandIn.Text == "tellraw " || commandIn.Text == "replaceitem entity " || cmdEqu_name))
                {
                    e.Handled = true;
                    int tempindex = 0;
                    if (c_playerList.Count() > 1 && c_playerList_index != 0)
                    {
                        tempindex = c_playerList_index - 1;
                    }
                    else
                    {
                        tempindex = c_playerList.Count() - 1;
                    }
                    commandIn.Text += c_playerList[tempindex].RemoveColorCodes() + " ";
                    commandIn.Select(commandIn.Text.Length, 0);
                    commandIn.ScrollToEnd();
                }
            }
            else if (e.Key == Key.Tab)  //自动补全功能
            {
                e.Handled = true;
                if (commandIn.Text.Length == 0) //tab键按序显示所有命令
                {
                    //载入cache
                    if (c_help.Count == 0)
                    {
                        tabCmdInit();
                    }
                    if (c_help.Count() != 0)
                    {
                        //显示
                        bool needAdd = tabContorlFlush(c_help, c_help_index);
                        if (needAdd)
                        {
                            c_help_index++;
                            if (c_help_index == c_help.Count())
                            {
                                c_help_index = 0;
                            }
                        }
                    }
                }
                //命令补全 - 1字符
                else if (commandIn.Text.Length == 1 && commandIn.Text != " ")
                {
                    //载入cache
                    if (c_help.Count == 0)
                    {
                        tabCmdInit();
                    }
                    if (c_help.Count != 0)
                    {
                        //筛选
                        if (c_help_search_temp != commandIn.Text)
                        {
                            c_help_search.Clear();
                            c_help_search_index = 0;
                            c_help_search_temp = commandIn.Text;
                            for (int i = 0; i < c_help.Count; i++)
                            {
                                if (c_help[i].RemoveColorCodes().Substring(0, 1) == c_help_search_temp)
                                {
                                    c_help_search.Add(c_help[i]);
                                }
                            }
                        }
                        //显示
                        bool needAdd = tabContorlFlush(c_help_search, c_help_search_index);
                        if (needAdd)
                        {
                            c_help_search_index++;
                            if (c_help_search_index == c_help_search.Count())
                            {
                                c_help_search_index = 0;
                            }
                        }
                    }
                }
                //命令补全 - 2字符
                else if (commandIn.Text.Length == 2 && commandIn.Text.Substring(1, 1) != " ")
                {
                    //载入cache
                    if (c_help.Count == 0)
                    {
                        tabCmdInit();
                    }
                    if (c_help.Count != 0)
                    {
                        //筛选
                        if (c_help_search_temp != commandIn.Text)
                        {
                            c_help_search.Clear();
                            c_help_search_index = 0;
                            c_help_search_temp = commandIn.Text;
                            for (int i = 0; i < c_help.Count; i++)
                            {
                                if (c_help[i].RemoveColorCodes().Substring(0, 2) == c_help_search_temp)
                                {
                                    c_help_search.Add(c_help[i]);
                                }
                            }
                        }
                        //显示
                        bool needAdd = tabContorlFlush(c_help_search, c_help_search_index);
                        if (needAdd)
                        {
                            c_help_search_index++;
                            if (c_help_search_index == c_help_search.Count())
                            {
                                c_help_search_index = 0;
                            }
                        }
                    }
                }
                //tab键序列显示玩家名列表
                else if (commandIn.Text == "ban " || commandIn.Text == "op " || commandIn.Text == "give " || commandIn.Text == "tellraw " || commandIn.Text == "replaceitem entity " || cmdEqu_name)
                {
                    //载入cache
                    string[] playerList = getSTDOUT("list").Split(':')[1].Split(',');
                    List<string> tempList = new List<string>();
                    for (int i = 0; i < playerList.Count(); i++)
                    {
                        tempList.Add(playerList[i].Replace(" ", string.Empty));
                    }
                    if (tempList.Count() < c_playerList.Count())
                    {
                        c_playerList_index = c_playerList.Count() - tempList.Count();
                        if (c_playerList_index < 0) c_playerList_index = 0;
                    }
                    c_playerList = tempList;
                    //显示
                    bool needAdd = tabContorlFlush(c_playerList, c_playerList_index);
                    if (needAdd)
                    {
                        c_playerList_index++;
                        if (c_playerList_index == c_playerList.Count())
                        {
                            c_playerList_index = 0;
                        }
                    }
                }
                //插入色彩符号
                else
                {
                    commandIn.Text += "§";
                    commandIn.Select(commandIn.Text.Length, 0);
                }
            }
            else if (Keyboard.Modifiers == ModifierKeys.Shift && e.Key == Key.D2)   //press @
            {

            }
        }

        private bool tabContorlFlush(List<string> c_List, int c_List_index)
        {
            bool needAdd = false;
            if (c_List_index != 0 && c_List.Count > 1)
            {
                //删除上一条
                data.RemoveAt(data.Count() - 1);
                setLog();
                readLog();
            }
            //else if (c_List.Count == 1)
            //{
            //    data.RemoveAt(data.Count() - 1);
            //    setLog();
            //    readLog();
            //}
            //
            if (c_List.Count != 0)
            {
                string tmp = "§l" + c_List[c_List_index] + " §r";
                if (c_List.Count() > 1)
                {
                    for (int i = 1; i < c_List.Count(); i++)
                    {
                        if (i < 9)
                        {
                            if (c_List_index + i < c_List.Count())
                            {
                                tmp += c_List[c_List_index + i] + " ";
                            }
                            else
                            {
                                tmp += c_List[0] + " ";
                                break;
                            }
                        }
                    }
                    tmp += "...";
                    //reset index
                    needAdd = true;
                }
                setLog(tmp);
                readLog();
            }
            else
            {
                setLog("未找到任何对应文本！");
                readLog();
            }
            return needAdd;
        }

        private void tabCmdInit()
        {
            setLog("Initializing...");
            try
            {
                Thread t = new Thread(() =>
                {
                    string header = getSTDOUT("?").Replace("\r", string.Empty).Split('\n')[0];
                    int pages = 0;
                    if (header.IndexOf("/") != -1) //spigot e.g. this function can't use in vanilla server.
                    {
                        pages = int.Parse(header.Split('/')[1].Substring(0, 3).Replace(")", string.Empty).Replace(" ", string.Empty));
                    }
                    if (pages != 0)
                    {
                        string[] help1List = getSTDOUT("? 1").Replace("\r", string.Empty).Split('\n');
                        string help1 = string.Empty;
                        if (help1List.Count() >= 3)
                        {
                            for (int i = 2; i < help1List.Count(); i++)
                            {
                                help1 += help1List[i] + "\r\n";
                            }
                        }
                        for (int i = 2; i <= pages; i++)
                        {
                            string[] help2List = getSTDOUT("? " + i).Replace("\r", string.Empty).Split('\n');
                            string help2 = string.Empty;
                            if (help2List.Count() >= 2)
                            {
                                for (int j = 1; j < help2List.Count(); j++)
                                {
                                    help1 += help2List[j] + "\r\n";
                                }
                            }
                            help1 += help2;
                        }
                        string[] temp = help1.Replace("\r", string.Empty).Split('\n');
                        for (int i = 0; i < temp.Count(); i++)
                        {
                            if (temp[i] != string.Empty && temp[i].Substring(2, 1) == "/")
                            {
                                string tmpStr = temp[i].Split(':')[0];
                                c_help.Add(tmpStr.Remove(2, 1));
                            }
                        }
                    }
                    tellMainThread = "Get command list finished! Please type command again.";
                });
                t.Start();
            }
            catch (Exception) { }
        }

        //cache
        private string c_help_search_temp = string.Empty;
        private string[] c_getSystemInfo = new string[3];
        private int c_help_index = 0;
        private List<string> c_help = new List<string>();
        private int c_help_search_index = 0;
        private List<string> c_help_search = new List<string>();
        private int c_playerList_index = 0;
        private List<string> c_playerList = new List<string>();

        private int historyIndex = 0;
        private List<string> data = new List<string>();
        private List<string> history = new List<string>();

        private void readLog()
        {
            List<string> temp = readCfg(Directory.GetCurrentDirectory() + @"\log.ini");
            if (temp != null)
            {
                data = temp;
            }
            AnalysisString anas = new AnalysisString();
            consoleOut.Document = anas.Analysis(data);
            consoleOut.ScrollToEnd();
        }

        private void setLog()
        {
            string temp = Directory.GetCurrentDirectory() + @"\log.ini";
            setCfg(temp, data);
        }

        private void setLog(string nowReturn)
        {
            data.Add(nowReturn);
            setLog();
        }

        private void readHistory()
        {
            List<string> temp = readCfg(Directory.GetCurrentDirectory() + @"\history.ini");
            if (temp != null)
            {
                history = temp;
            }
        }

        private void setHistory()
        {
            string temp = Directory.GetCurrentDirectory() + @"\history.ini";
            setCfg(temp, history);
        }

        private void setHistory(string nowInput)
        {
            history.Add(nowInput);
            setHistory();
        }

        public List<string> readCfg(string path)
        {
            List<string> txt = new List<string>();
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    int lineCount = 0;
                    while (sr.Peek() > 0)
                    {
                        lineCount++;
                        string temp = sr.ReadLine();
                        txt.Add(temp);
                    }
                }
                return txt;
            }
            else
            {
                return null;
            }
        }

        public void setCfg(string path, List<string> value)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    for (int i = 0; i < value.Count(); i++)
                    {
                        if (value[i] != string.Empty)
                        {
                            sw.WriteLine(value[i]);
                        }
                    }
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
    }
}
