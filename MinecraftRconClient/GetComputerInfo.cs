using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Net;
using System.Text.RegularExpressions;

namespace MinecraftRconClient
{
    class GetComputerInfo
    {
        public string[] GetInfo()
        {
            string UsedMemory = "",
                FreeMemory = "0",
                TotalMemory = "0",
                SystemType = "",
                UserName = "",
                CPUID = "",
                OSVersionStr = "",
                OSVersion = "",
                OSVer = "",
                NetMACAddress = "",
                CPUInfo = "",
                CPUInfo2 = "",
                CPUInfo3 = "",
                AddressIP = "",
                Time = "";

            List<string> Graphics = new List<string>();
            List<string> NetworkMac = new List<string>();

            try
            {
                ManagementClass mc1 = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc1 = mc1.GetInstances();
                foreach (ManagementObject mo in moc1)
                {
                    TotalMemory = mo["TotalPhysicalMemory"].ToString();
                }
                foreach (ManagementObject mo in moc1)
                {
                    SystemType = mo["SystemType"].ToString();
                }
                foreach (ManagementObject mo in moc1)
                {
                    UserName = mo["UserName"].ToString();
                }
                moc1.Dispose();
                mc1.Dispose();
            }
            catch (Exception) { }

            try
            {
                ManagementClass mc2 = new ManagementClass("win32_processor");
                ManagementObjectCollection moc2 = mc2.GetInstances();
                foreach (ManagementObject mo in moc2)
                {
                    CPUID = mo["processorid"].ToString();
                }
                moc2.Dispose();
                mc2.Dispose();
            }
            catch (Exception) { }

            try
            {
                OSVersionStr = Environment.OSVersion.VersionString.ToString();

                Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion");
                OSVersion = rk.GetValue("ProductName").ToString();
                OSVer = rk.GetValue("CurrentBuildNumber").ToString();
                rk.Close();
            }
            catch (Exception){ }

            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        NetworkMac.Add(mo["MacAddress"].ToString());
                    }
                }
                if (NetworkMac.Count > 0)
                {
                    for (int i = 0; i < NetworkMac.Count; i++)
                    {
                        if (NetworkMac[i].Substring(0, 2) != "00")
                        {
                            NetMACAddress = NetworkMac[i];
                        }
                    }
                }
            }
            catch (Exception) { }

            try
            {
                ManagementClass mc3 = new ManagementClass("Win32_PerfFormattedData_PerfOS_Memory");
                ManagementObjectCollection moc3 = mc3.GetInstances();
                foreach (ManagementObject mo in moc3)
                {
                    FreeMemory = mo["AvailableBytes"].ToString();
                }
                moc3.Dispose();
                mc3.Dispose();
            }
            catch (Exception) { }

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject obj2 in searcher.Get())
                {
                    CPUInfo = obj2.GetPropertyValue("Name").ToString();
                    CPUInfo2 = obj2.GetPropertyValue("CurrentClockSpeed").ToString();// + " Mhz";
                    if (CPUInfo.IndexOf('@') == -1)
                    {
                        CPUInfo += " @ " + CPUInfo2;
                    }
                    CPUInfo3 = Environment.ProcessorCount.ToString();// + " 核处理器";
                }
            }
            catch (Exception) { }

            try
            {
                AddressIP = GetPublicIP2();
            }
            catch (Exception)
            {
                AddressIP = GetPublicIP();
            }

            try
            {
                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("Select * from Win32_VideoController");
                foreach (ManagementObject mo in searcher2.Get())
                {
                    //http://blog.csdn.net/asdcsj/article/details/50035403
                    Graphics.Add(mo["Name"].ToString());
                }
            }
            catch (Exception) { }
            if (Graphics.Count <= 1)
            {
                Graphics.Add("Can't get another Graphic Card info!");
                Graphics.Add("Can't get another Graphic Card info!");
            }

            try
            {
                Time = fixTime();
            }
            catch (Exception) { }

            try
            {
                UsedMemory = (long.Parse(TotalMemory) - long.Parse(FreeMemory)).ToString();
            }
            catch (Exception) { }

            string restr =
                    "§+                                  ..,      §r" +
                "\r\n§+                      ....,,:;+ccllll      §r" + "§e> §6" + UserName.Split('\\')[1] + "§r@§6" + Environment.GetEnvironmentVariable("ComputerName") +
                "\r\n§+        ...,,+:;  cllllllllllllllllll      §r" + "§6Kernel: §r" + OSVersionStr +
                "\r\n§+  ,cclllllllllll  lllllllllllllllllll      §r" + "§6OS: §r" + OSVersion + " " + OSVer +
                "\r\n§+  llllllllllllll  lllllllllllllllllll      §r" + "§6SystemType: §r" + SystemType +
                "\r\n§+  llllllllllllll  lllllllllllllllllll      §r" + "§6CPU: §r" + CPUInfo.Split('@')[0] + "§7@§r " + CPUInfo3 + " §7x§r" + CPUInfo.Split('@')[1] +
                "\r\n§+  llllllllllllll  lllllllllllllllllll      §r" + "§6CPUID: §r" + CPUID +
                "\r\n§+  llllllllllllll  lllllllllllllllllll      §r" + "§6Network: §r" + AddressIP + " §7@§r " + NetMACAddress +
                "\r\n§+  llllllllllllll  lllllllllllllllllll      §r" + "§6Memory: §r" + Math.Round((double.Parse(UsedMemory) / 1024 / 1024 / 1024), 2) + " §7GB§r / " + Math.Round((double.Parse(TotalMemory) / 1024 / 1024 / 1024), 2) + " §7GB§r" +
                "\r\n§+                                           §r" + "§6Uptime: §r"; string restr2 = Time; string restr3 = 
                "\r\n§+  llllllllllllll  lllllllllllllllllll      §r" + "§6Graphics1: §r" + Graphics[0] + 
                "\r\n§+  llllllllllllll  lllllllllllllllllll      §r" + "§6Graphics2: §r" + Graphics[1] + 
                "\r\n§+  llllllllllllll  lllllllllllllllllll      §r" + "§6ColorTest: §r" + 
                "\r\n§+  llllllllllllll  lllllllllllllllllll      §r" + "  §l§0l§00  §l§1l§11  §l§2l§22  §l§3l§33  §l§4l§44  §l§5l§55  §l§6l§66  §l§7l§77" + 
                "\r\n§+  llllllllllllll  lllllllllllllllllll      §r" + "  §l§8l§88  §l§9l§99  §l§al§aa  §l§bl§bb  §l§cl§cc  §l§dl§dd  §l§el§ee  §l§fl§ff" + 
                "\r\n§+  ,cclllllllllll  lllllllllllllllllll      §r" + "  §1测 §2试 §l§3文 §l§4本 §5測 §6試 §l§7文 §l§8本" +
                "\r\n§+        ...,,+:;  cllllllllllllllllll      §r" + "  §9試 §l§a験 §l§bテ §cキ §dス §eト" +
                "\r\n§+                      ....,,:;+ccllll      §r" + 
                "\r\n§+                                  ..,      §r"
                ;
            return new string[] { restr, restr2, restr3 };
        }

        public string fixTime()
        {
            string Time = string.Empty;
            Time = Environment.TickCount.ToString();
            Time = Math.Round(double.Parse(Time) / 1000d / 60d / 60d) + "§7h§r " + Math.Round(double.Parse(Time) / 1000d / 60d - int.Parse(Time) / 1000 / 60 / 60 * 60) + "§7m§r " + Math.Round(double.Parse(Time) / 1000d - int.Parse(Time) / 1000 / 60 * 60) + "§7s§r";
            return Time;
        }

        private string GetPublicIP()
        {
            String direction = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                direction = stream.ReadToEnd();
            }

            //Search for the ip in the html
            int first = direction.IndexOf("Address: ") + 9;
            int last = direction.LastIndexOf("</body>");
            direction = direction.Substring(first, last - first);

            return direction;
        }

        public static string GetPublicIP2()
        {
            string tempip = "";
            WebRequest request = WebRequest.Create("http://ip.qq.com/");
            request.Timeout = 10000;
            WebResponse response = request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(resStream, System.Text.Encoding.Default);
            string htmlinfo = sr.ReadToEnd();
            //匹配IP的正则表达式
            Regex r = new Regex("((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|\\d)\\.){3}(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|[1-9])", RegexOptions.None);
            Match mc = r.Match(htmlinfo);
            //获取匹配到的IP
            tempip = mc.Groups[0].Value;

            resStream.Close();
            sr.Close();
            return tempip;
        }
    }
}
