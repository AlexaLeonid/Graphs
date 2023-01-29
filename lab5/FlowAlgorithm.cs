using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace lab5
{
    class FlowAlgorithm : Tool
    {
        Canvas CnvBack;
        DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
        private List<Vertex> graph;
        StringBuilder sb = new StringBuilder();
        List<StringBuilder> Logger = new List<StringBuilder>();
        List<Vertex> FlowPath;
        int[,] cons;
        int start;
        int end;
        int sum = 0;
        bool flag = true;
        int min;
        //  bool Done= true;
        int through;
        string path;
        public FlowAlgorithm(Canvas CnvBack)
        {
            graph = Vertices;
            this.CnvBack = CnvBack;
            cons = new int[graph.Count, graph.Count];
            FileHelper.Reset(CnvBack);
            CnvBack.Children.Remove(TBLogger);
            TBLogger.Text = "";
            CnvBack.Children.Add(TBLogger);
        //    Execute();
        }
        public List<Vertex> getGraph()
        {
            return this.graph;
        }
        private void myDFS(int current, int end, List<int> visVer)
        {
            if (flag)
            {
                return;
            }
            sb.Append($"\nПогружаемся в точку {graph[current].getLabel()}");
            //   Logger.Add(new StringBuilder());
            Logger[Logger.Count - 1].Append($"\nПогружаемся в точку {graph[current].getLabel()}");
            visVer.Add(current);

            if (current == end)
            {
                flag = true;
                min = 9999;
                for (int i = 0; i < visVer.Count - 1; i++)
                {
                    Dictionary<Vertex, Edge> vert1 = graph[visVer[i]].getEdges();
                    if (vert1[graph[visVer[i + 1]]].getWeight() - cons[visVer[i], visVer[i + 1]] < min)
                        min = vert1[graph[visVer[i + 1]]].getWeight() - cons[visVer[i], visVer[i + 1]];
                }
                path = "";
                for (int i = 0; i < visVer.Count; i++)
                {
                    path += graph[visVer[i]].getLabel() + " ";
                    FlowPath.Add(graph[visVer[i]]);
                }
                FindThrough();
                sb.Append($"\nПуть {path} найден пропускная способность {min}");
                Logger[Logger.Count - 1].Append($"\nПуть {path}, найден пропускная способность {min}");
                sum += min;
                for (int i = 0; i < visVer.Count - 1; i++)
                {
                    cons[visVer[i], visVer[i + 1]] += min;
                    cons[visVer[i + 1], visVer[i]] += min;
                }
                return;
            }
            Dictionary<Vertex, Edge> vert = graph[current].getEdges();
            for (int i = 0; i < graph.Count; i++)
            {
                bool next = true;
                for (int i1 = 0; i1 < visVer.Count; i1++)
                {
                    if (visVer[i1] == i)
                        next = false;
                }
                if (current != i && vert.ContainsKey(graph[i]) && vert[graph[i]].getWeight() - cons[current, i] != 0 && next)
                    myDFS(i, end, visVer);
            }
            visVer.RemoveAt(visVer.Count - 1);
        }

        public void run()
        {
            foreach(RectConnection c in ConnectionsInfo)
            {
         //       Canvas.SetZIndex(c.LPath, 15);
                Canvas.SetTop(c.LPath, Canvas.GetTop(c.WeightL));
                Canvas.SetLeft(c.LPath, Canvas.GetLeft(c.WeightL) + 6);
                c.LPath.Content = ", 0/0";
                CnvBack.Children.Add(c.LPath);
            }
            start = 0;
            end = graph.Count - 1;
            Logger.Add(new StringBuilder());
            sb.Append($"\nПоиск максимального потока из {graph[start].getLabel()} в {graph[end].getLabel()}");
            Logger[Logger.Count - 1].Append($"Поиск максимального потока из {graph[start].getLabel()} в {graph[end].getLabel()}");
            TBLogger.Text += Logger[Logger.Count - 1].ToString();
            /*   while (true)
               {
                   Logger.Add(new StringBuilder());
                   sb.Append($"\nПытаемся найти новый путь");
                   ResetDraw();
                   Logger[Logger.Count - 1].Append($"\n\nПытаемся найти новый путь");
                   flag = false;
                   FlowPath = new List<Vertex>();
                   myDFS(start, end, new List<int>());
                   if (!flag)
                       break;
               }*/


            timer.Tick += Timer_Tick;
            timer.Start();

            // sb.Append("\nДобавлаем следующую вершину " + nextVertex.getLabel() + " с минимальным ребром " + nextMinimum.getWeight());
            // Logger[Logger.Count - 1].Append("\nДобавлаем следующую вершину " + nextVertex.getLabel() + " с минимальным ребром " + nextMinimum.getWeight());
            /*   Logger.Add(new StringBuilder());
               sb.Append($"\nПутей больше не осталось \nМаксимальный поток равен {sum}");
               Logger[Logger.Count - 1].Append($"\nПутей больше не осталось \nМаксимальный поток равен {sum}");
               TBLogger.Text += Logger[Logger.Count - 1].ToString();
               string path = "C:\\Users\\Sasacompik\\OneDrive\\Рабочий стол\\Алгоритм и анализ сложности\\lab5\\LoggerFlowAlgorithm.txt";
               File.WriteAllText(path, string.Empty);
               File.AppendAllText(path, sb.ToString());*/
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (flag)
            {
                Logger.Add(new StringBuilder());
                sb.Append($"\nПытаемся найти новый путь");
                Logger[Logger.Count - 1].Append($"\n\nПытаемся найти новый путь");
                flag = false;
                FlowPath = new List<Vertex>();
                myDFS(start, end, new List<int>());
                if (FlowPath.Count > 0)
                {
                    ResetDraw();
                  //  FindThrough();
                    DrawVertices();
                }
                TBLogger.Text += Logger[Logger.Count - 1].ToString();
            }
            else
            {
                //   TBLogger.Text += "\nПройденный путь: " + GetPath(startV, finishV);
                timer.Stop();
               
                Logger.Add(new StringBuilder());
                sb.Append($"\nПутей больше не осталось \nМаксимальный поток равен {sum}");
                //   Logger[Logger.Count - 1].Append($"\nПутей больше не осталось \nМаксимальный поток равен {sum}");
                Logger[Logger.Count - 1].Append($"\nПутей больше не осталось \n\nДлина максимального потока равна: {sum}");//\nПропускная способность: {min}\nПуть: {path}");
                TBLogger.Text += Logger[Logger.Count - 1].ToString();
                string pathFile = "C:\\Users\\Пользователь\\Downloads\\lab5\\lab5\\LoggerFlowAlgorithm.txt";
                File.WriteAllText(pathFile, string.Empty);
                File.AppendAllText(pathFile, sb.ToString());
            }
        }
        public void DrawVertices()
        {
            foreach (Relative r in Relatives)
            {
                if (r.vertex == FlowPath[0])
                {
                    r.rectangle.Stroke = Brushes.Coral;
                    r.rectangle.Fill = Brushes.AliceBlue;
                }
            }
            for (int i = 1; i < FlowPath.Count; i++)
            {
                foreach (Relative r in Relatives)
                {
                    if (r.vertex == FlowPath[i])
                    {
                        //   TBLogger.Text += Logger[i];
                        r.rectangle.Stroke = Brushes.Coral;
                        r.rectangle.Fill = Brushes.AliceBlue;
                        Relative sr = null;
                        foreach (Relative re in Relatives)
                        {
                            if (re.vertex == FlowPath[i - 1])
                            {
                                sr = re;
                            }
                        }

                        foreach (RectConnection c in ConnectionsInfo)
                        {
                            if (sr != null)
                            {
                                if ((c.StartRect == r.rectangle && c.FinishRect == sr.rectangle) || (c.FinishRect == r.rectangle && c.StartRect == sr.rectangle))
                                {
                                    c.Line.Stroke = Brushes.Coral;
                                    int con = int.Parse(c.LPath.Content.ToString()[2].ToString()) + min;
                                    c.LPath.Content = ", " + con + "/" + c.WeightL.Content;
                                }
                            }
                        }
                    }
                }
            }
        }
        public void FindThrough()
        {
            through = int.MaxValue;
          /*  foreach (Relative r in Relatives)
            {
                if (r.vertex == FlowPath[0])
                {
                    r.rectangle.Stroke = Brushes.Coral;
                    r.rectangle.Fill = Brushes.AliceBlue;
                }
            }*/
            for (int i = 1; i < FlowPath.Count; i++)
            {
                foreach (Relative r in Relatives)
                {
                    if (r.vertex == FlowPath[i])
                    {
                        //   TBLogger.Text += Logger[i];
                      /*  r.rectangle.Stroke = Brushes.Coral;
                        r.rectangle.Fill = Brushes.AliceBlue;*/
                        Relative sr = null;
                        foreach (Relative re in Relatives)
                        {
                            if (re.vertex == FlowPath[i - 1])
                            {
                                sr = re;
                            }
                        }

                        foreach (RectConnection c in ConnectionsInfo)
                        {
                            if (sr != null)
                            {
                                if ((c.StartRect == r.rectangle && c.FinishRect == sr.rectangle) || (c.FinishRect == r.rectangle && c.StartRect == sr.rectangle))
                                {
                                    //c.Line.Stroke = Brushes.Coral;
                                    //c.LPath.Content = ", " + min + "/" + c.WeightL.Content;
                                    if(int.Parse(c.WeightL.Content.ToString()) < through)
                                    {
                                        through = int.Parse(c.WeightL.Content.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void ResetDraw()
        {
            foreach (Relative r in Relatives)
            {
                r.rectangle.Stroke = Brushes.Gray;
                r.rectangle.Fill = Brushes.Bisque;
            }
            foreach (RectConnection c in ConnectionsInfo)
            {
                c.Line.Stroke = Brushes.Green;
            }
        }
        /*        bool isDisconnected()
                {
                    foreach (Vertex vertex in graph)
                    {
                        if (!vertex.getVisited())
                        {
                            return true;
                        }
                    }
                    return false;
                }

                public String originalGraphToString()
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Vertex vertex in graph)
                    {
                        sb.Append(vertex.originalToString());
                    }
                    return sb.ToString();
                }

                public void resetPrintHistory()
                {
                    foreach (Vertex vertex in graph)
                    {
                        foreach (var pair in vertex.getEdges())
                        {
                            pair.Value.setPrinted(false);
                        }
                    }
                }

                public string minimumSpanningTreeToString()
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Vertex vertex in graph)
                    {
                        sb.Append(vertex.includedToString());
                    }
                    return sb.ToString();
                }
                public List<StringBuilder> minimumSpanningTree()
                {
                    foreach (Vertex vertex in graph)
                    {
                        vertex.includedToTree();
                    }
                    return Logger;
                }*/
    }
}
