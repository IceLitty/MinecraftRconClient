using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media;

namespace MinecraftRconClient
{
    class AnalysisString
    {
        public FlowDocument Analysis(List<string> stdoutList)
        {
            string main = string.Empty;
            for (int i = 0; i < stdoutList.Count(); i++)
            {
                main += stdoutList[i] + Environment.NewLine;
            }
            return Analysis(main);
        }

        public FlowDocument Analysis(string stdoutStr)
        {
            FlowDocument document = new FlowDocument();
            document.LineHeight = 1d;
            Brush brB = new SolidColorBrush(Color.FromRgb(10, 40, 51));
            Brush br0 = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            Brush br1 = new SolidColorBrush(Color.FromRgb(0, 0, 170));
            Brush br2 = new SolidColorBrush(Color.FromRgb(0, 170, 0));
            Brush br3 = new SolidColorBrush(Color.FromRgb(0, 170, 170));
            Brush br4 = new SolidColorBrush(Color.FromRgb(170, 0, 0));
            Brush br5 = new SolidColorBrush(Color.FromRgb(170, 0, 170));
            Brush br6 = new SolidColorBrush(Color.FromRgb(255, 170, 0));
            Brush br7 = new SolidColorBrush(Color.FromRgb(170, 170, 170));
            Brush br8 = new SolidColorBrush(Color.FromRgb(85, 85, 85));
            Brush br9 = new SolidColorBrush(Color.FromRgb(85, 85, 255));
            Brush bra = new SolidColorBrush(Color.FromRgb(85, 255, 85));
            Brush brb = new SolidColorBrush(Color.FromRgb(85, 255, 255));
            Brush brc = new SolidColorBrush(Color.FromRgb(255, 85, 85));
            Brush brd = new SolidColorBrush(Color.FromRgb(255, 85, 255));
            Brush bre = new SolidColorBrush(Color.FromRgb(255, 255, 85));
            Brush brf = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            Brush brWin = new SolidColorBrush(Color.FromRgb(0, 120, 215));
            Brush br = brf; //default color
            stdoutStr = stdoutStr.Replace("\n", string.Empty);
            string[] split = stdoutStr.Split('\r');
            for (int i = 0; i < split.Count(); i++)
            {
                var paragraph = new Paragraph(); //line, like <p>
                string[] sptemp = split[i].Split('§');
                if (sptemp.Count() > 1)
                {
                    var run = new Run(sptemp[0]);
                    run.Foreground = br;
                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                    paragraph.Inlines.Add(run);
                    for (int j = 1; j < sptemp.Count(); j++)
                    {
                        if (sptemp[j].Substring(0, 1) == "0")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br0;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "1")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br1;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "2")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br2;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "3")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br3;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "4")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br4;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "5")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br5;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "6")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br6;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "7")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br7;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "8")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br8;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "9")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br9;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "a")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = bra;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "b")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = brb;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "c")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = brc;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "d")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = brd;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "e")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = bre;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "f")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = brf;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "k")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "l")  //加粗
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                            paragraph.Inlines.Add(run);
                            if (j + 1 != sptemp.Count())
                            {
                                if (sptemp[j + 1].Substring(0, 1) == "0")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = br0;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "1")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = br1;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "2")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = br2;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "3")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = br3;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "4")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = br4;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "5")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = br5;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "6")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = br6;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "7")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = br7;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "8")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = br8;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "9")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = br9;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "a")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = bra;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "b")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = brb;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "c")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = brc;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "d")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = brd;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "e")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = bre;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                                else if (sptemp[j + 1].Substring(0, 1) == "f")
                                {
                                    run = new Run(sptemp[j + 1].Substring(1, sptemp[j + 1].Length - 1));
                                    run.Foreground = brf;
                                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Bold");
                                    paragraph.Inlines.Add(run);
                                    j++;
                                }
                            }
                        }
                        else if (sptemp[j].Substring(0, 1) == "m")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "n")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "o")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "r")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = br;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else if (sptemp[j].Substring(0, 1) == "+")
                        {
                            run = new Run(sptemp[j].Substring(1, sptemp[j].Length - 1));
                            run.Foreground = brWin;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                        else
                        {
                            run = new Run(sptemp[j]);
                            run.Foreground = br;
                            run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                            paragraph.Inlines.Add(run);
                        }
                    }
                }
                else
                {
                    var run = new Run(split[i]);
                    run.Foreground = br;
                    run.FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/"), "./#CPMono_v07 Plain");
                    paragraph.Inlines.Add(run);
                }
                document.Blocks.Add(paragraph);
            }
            document.Background = brB;
            return document;
        }
    }
}
