using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace lab5
{
    internal class DFS: Tool
    {
        DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        int i = 1;
        List<(Vertex,Vertex)> vertList = new List<(Vertex,Vertex)>();
        StringBuilder sb = new StringBuilder();
        List<StringBuilder> Logger = new List<StringBuilder>();
        Canvas CnvBack;
        List<Vertex> Path = new List<Vertex>();
        Stack<Vertex> stack = new Stack<Vertex>();
        List<String> PrintStack = new List<string>();
        public DFS(Canvas CnvBack)
        {
            this.CnvBack = CnvBack;
            FileHelper.Reset(CnvBack);
            CnvBack.Children.Remove(TBLogger);
            TBLogger.Text = "";
            CnvBack.Children.Add(TBLogger);
            Execute(Vertices[0], Vertices[Vertices.Count-1]);
        }
        void PrintVert(Vertex Vert)
        {
            sb.Append($"\n\nВершина {Vert.getLabel()}. Смежна с вершинами:");
            Logger[Logger.Count - 1].Append($"\n\nВершина {Vert.getLabel()}. Смежна с вершинами:");
            foreach (var v in Vert.getEdges())
            {
                sb.Append($"  {v.Key.getLabel()}");
                Logger[Logger.Count - 1].Append($"  {v.Key.getLabel()}");
            }
        }
        public List<(Vertex,Vertex)> PrintDeep()
        {
            FileHelper.Reset(CnvBack);
            int verticesCount = Vertices.Count;

            Stack<(Vertex,Vertex)> vertStack = new Stack<(Vertex,Vertex)>();
            vertList.Add((null, Vertices[0]));
        //    Logger.Add(new StringBuilder());
            for (int vert = 0; vert < verticesCount; vert++)
            {
                Vertex vertCurr = Vertices[vert];
                Logger.Add(new StringBuilder());
                while (true)
                {
                    if (vertCurr.getVisited() == false)
                    {
                        Path.Add(vertCurr);
                        PrintVert(vertCurr);
                        
                        vertCurr.setVisited(true);

                        foreach (var v in vertCurr.getEdges())
                        {
                            if(v.Key.getVisited() == false)
                            {
                                vertStack.Push((vertCurr, v.Key));
                                PrintStack.Add(v.Key.getLabel());
                                sb.Append("\nДобавляем в стэк " + v.Key.getLabel());
                                Logger[Logger.Count - 1].Append("\nДобавляем в стэк " + v.Key.getLabel());
                            }
                        }
                    }
 

                    if (vertStack.Count == 0)
                    {
                        break;
                    }

                    vertList.Add(vertStack.Pop());
                    PrintStack.Remove(PrintStack[PrintStack.Count - 1]);
                    vertCurr = vertList[vertList.Count-1].Item2;
                    Logger.Add(new StringBuilder());
                    sb.Append("\nДостаем из стэка " + vertCurr.getLabel());
                    Logger[Logger.Count - 1].Append("\nДостаем из стэка " + vertCurr.getLabel());
                    if (vertCurr.getVisited())
                    {
                        sb.Append(". Эту вершину уже посещали");
                        Logger[Logger.Count - 1].Append(". Эту вершину уже посещали");
                    }
                    Logger[Logger.Count - 1].Append($"\nСостояние стека: ");
                    for (int j = 0; j < PrintStack.Count; j++)
                    {
                        Logger[Logger.Count - 1].Append(" " + PrintStack[j]);
                    }
                    Logger[Logger.Count - 1].Append($"\n\nПройденный путь: ");
                    foreach (var v in Path)
                    {
                        // sb.Append($"  {v.Key.getLabel()}");
                        Logger[Logger.Count - 1].Append($"  {v.getLabel()}");
                    }
                }
                
            }

         //   string path = "C:\\Users\\Sasacompik\\OneDrive\\Рабочий стол\\Алгоритм и анализ сложности\\lab5\\LoggerWidth.txt";
            sb.Remove(0, 2);
            Logger[0].Remove(0, 2);
        //    File.WriteAllText(path, string.Empty);
        //    File.AppendAllText(path, sb.ToString());
            return vertList;
        }
      /*  public bool DoLength(Vertex v, Vertex t)
        {
            // v - посещенный узел (вершина)
            // t - пункт назначения

            // это общие случаи
            // либо достигли пункта назначения, либо уже посещали узел
            if (v == t)
            {
                stack.Push(v);
             //   sb.AppendLine("Достигли пункта назначения");
                string path = "C:\\Users\\Sasacompik\\OneDrive\\Рабочий стол\\Алгоритм и анализ сложности\\lab5\\Logger.txt";
                File.WriteAllText(path, string.Empty);
                File.AppendAllText(path, sb.ToString());
                return true;
            }

            if (v.getVisited()) return false;

            // помечаем узел как посещенный
            v.setVisited(true);
           // sb.AppendLine("помечаем узел как посещенный");
            // исследуем всех соседей (ближайшие соседние вершины) v
         //   sb.AppendLine("исследуем всех соседей (ближайшие соседние вершины) v");
            foreach (var neighbor in v.getEdges())
            {
                // если сосед не посещался
                if (!neighbor.Key.getVisited())
                {
                    // двигаемся по пути и проверяем, не достигли ли мы пункта назначения
                    bool reached = DoLength(neighbor.Key, t);
                    // возвращаем true, если достигли
                    if (reached)
                    {
                        stack.Push(v);
                      //  Console.WriteLine(v.getLabel());
                        return true;

                    }
                }
            }
            // если от v до t добраться невозможно
            return false;
        }*/
        public void Execute(Vertex v, Vertex t)
        {
          //  if(DoLength(v, t))
            {
                TBLogger.Text = "";
                vertList = PrintDeep();
                //  Vertex curV = stack.Pop();
                Vertex curV = vertList[0].Item2;
                foreach (Relative r in Relatives)
                {
                    if (r.vertex == curV)
                    {
                        r.rectangle.Stroke = Brushes.Coral;
                        r.rectangle.Fill = Brushes.AliceBlue;
                    }
                }
                TBLogger.Text += Logger[0].ToString();
                timer.Tick += Timer_Tick;
                timer.Start();
              /*  while (stack.Count > 0)
                {
                    //  Console.Write(stack.Pop().getLabel() + " ");
                    Vertex exV = curV;
                    curV = stack.Pop();
                    foreach (Relative r in Relatives)
                    {
                        if (r.vertex == curV)
                        {
                            r.rectangle.Stroke = Brushes.Coral;
                            r.rectangle.Fill = Brushes.AliceBlue;
                            // Thread.Sleep(100);
                            Relative sr = null;
                            foreach (Relative re in Relatives)
                            {
                                if (re.vertex == exV)
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
                                    }
                                }
                            }
                            // Thread.Sleep(1000);
                        }
                    }
                }*/
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (i < vertList.Count)
            {
                foreach (Relative r in Relatives)
                {
                    if (r.vertex == vertList[i].Item2)
                    {
                        TBLogger.Text += Logger[i].ToString();
                        r.rectangle.Stroke = Brushes.Coral;
                        r.rectangle.Fill = Brushes.AliceBlue;
                        Relative sr = null;
                        foreach (Relative re in Relatives)
                        {
                            if (re.vertex == vertList[i].Item1)
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
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                timer.Stop();
            }

            i++;
        }
    }
}
