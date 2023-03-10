using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace lab5
{
    internal class BFS: Tool
    {
        DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        int i = 1;
        Canvas CnvBack;
        Queue<Vertex> result = new Queue<Vertex>();
        StringBuilder sb = new StringBuilder();
        List<(Vertex, Vertex)> vertList = new List<(Vertex, Vertex)>();
        List<StringBuilder> Logger = new List<StringBuilder>();
        List<Vertex> Path = new List<Vertex>();
        List<String> PrintQueue = new List<string> ();
        public BFS(Canvas CnvBack)
        {
            this.CnvBack = CnvBack;
            FileHelper.Reset(CnvBack);
            CnvBack.Children.Remove(TBLogger);
            TBLogger.Text = "";
            CnvBack.Children.Add(TBLogger);
            Execute(Vertices[0], Vertices[Vertices.Count - 1]);
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
        public List<(Vertex, Vertex)> PrintWidth()
        {
            FileHelper.Reset(CnvBack);
            int verticesCount = Vertices.Count;

            Queue<(Vertex,Vertex)> vertQueue = new Queue<(Vertex,Vertex)>();
            vertList.Add((null, Vertices[0]));
           
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
                            if (v.Key.getVisited() == false)
                            {
                                vertQueue.Enqueue((vertCurr, v.Key));
                                PrintQueue.Add(v.Key.getLabel());
                                sb.Append("\nДобавляем в очередь " + v.Key.getLabel());
                                Logger[Logger.Count - 1].Append("\nДобавляем в очередь " + v.Key.getLabel());
                            }
                        }
                    }

                    if (vertQueue.Count == 0)
                        break;

                    vertList.Add(vertQueue.Dequeue());
                    PrintQueue.Remove(PrintQueue[0]);
                    vertCurr = vertList[vertList.Count - 1].Item2;
                    Logger.Add(new StringBuilder());
                    sb.Append("\nДостаем из очереди " + vertCurr.getLabel());
                    Logger[Logger.Count - 1].Append("\nДостаем из очереди " + vertCurr.getLabel());
                    if (vertCurr.getVisited())
                    {
                        sb.Append(". Эту вершину уже посещали");
                        Logger[Logger.Count - 1].Append(". Эту вершину уже посещали");
                    }
                    Logger[Logger.Count - 1].Append($"\nСостояние очереди: ");
                    for(int j = 0; j < PrintQueue.Count; j++)
                    {
                        Logger[Logger.Count - 1].Append(" " + PrintQueue[j]);
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
            sb.Remove(0,2);
            Logger[0].Remove(0,2);
        //    File.WriteAllText(path, string.Empty);
        //    File.AppendAllText(path, sb.ToString());
            return vertList;
        }
     /*   public bool DoWidth(Vertex s, Vertex t)
        {
            // s - начальная вершина
            // t - пункт назначения

            // инициализируем очередь
            foreach (Vertex v in Vertices)
            {
                v.setVisited(false);
            }
            Queue<Vertex> queue = new Queue<Vertex>();

            // добавляем s в очередь
            queue.Enqueue(s);
            sb.Append("Добавляем " + s.getLabel() + " в очередь");
            // помечаем s как посещенную вершину во избежание повторного добавления в очередь
            s.setVisited(true);

            while (queue.Count > 0)
            {
                // удаляем первый (верхний) элемент из очереди
                Vertex v = queue.Dequeue();
                sb.Append("\nУдаляем " + v.getLabel() +" элемент из очереди");
                result.Enqueue(v);
                //Console.Write(v.getLabel() + ": ");
                // abj[v] - соседи v
                sb.Append("\nСмотрим соседей вершины " + v.getLabel());
                foreach (var neighbor in v.getEdges())
                {
                    // если сосед не посещался
                    if (!neighbor.Key.getVisited())
                    {
                       // Console.Write(neighbor.Key.getLabel() + " ");
                        result.Enqueue(neighbor.Key);
                        // добавляем его в очередь
                        queue.Enqueue(neighbor.Key);
                        // помечаем вершину как посещенную
                        neighbor.Key.setVisited(true);
                        // если сосед является пунктом назначения, мы победили
                        if (neighbor.Key == t)
                        {
                            return true;
                        }

                    }
                }
                // если t не обнаружено, значит пункта назначения достичь невозможно
                Console.WriteLine();
            }
            return false;
        }*/

        public void Execute(Vertex v, Vertex t)
        {
           // if (DoWidth(v, t))
            {
                vertList = PrintWidth();
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
                /*while (result.Count > 0)
                {
                    //  Console.Write(stack.Pop().getLabel() + " ");
                    Vertex exV = curV;
                    curV = result.Dequeue();
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
