using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace lab5
{
    internal class Dijkstra: Tool
    {
        Canvas CnvBack;
        private StringBuilder DataText;
        private TextBox StartVBox;
        private TextBox FinishVBox;
        private TextBox WeightBox;
        private TextBox LPathBox;
        private Button BtnSave;
        private StackPanel DataPanel;
        Vertex startV;
        Vertex finishV;
        List<Vertex> result = new List<Vertex>();
        List<StringBuilder> Logger = new List<StringBuilder>();
        bool Done = true;
        DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
        //  Vertex BeginPoint;
        public Dijkstra(Canvas CnvBack)
        {
            this.CnvBack = CnvBack;
            CnvBack.Children.Remove(TBLogger);
            TBLogger.Text = "";
            CnvBack.Children.Add(TBLogger);
            FileHelper.Reset(CnvBack);
            DoPanel();
        }

        //   Graph graph;

      //  List<GraphVertexInfo> infos;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="graph">Граф</param>

        /// <summary>
        /// Инициализация информации
        /// </summary>
        void InitInfo()
        {

            infos = new List<GraphVertexInfo>();
            foreach (var r in Relatives)
            {
                infos.Add(new GraphVertexInfo(r.vertex));
                infos[infos.Count - 1].Relative = r;
                Canvas.SetZIndex(infos[infos.Count - 1].WeightSum, 15);
                Canvas.SetTop(infos[infos.Count - 1].WeightSum, Canvas.GetTop(r.rectangle) - 20);
                Canvas.SetLeft(infos[infos.Count - 1].WeightSum, Canvas.GetLeft(r.rectangle));
                infos[infos.Count - 1].WeightSum.Content = "∞";
                CnvBack.Children.Add(infos[infos.Count - 1].WeightSum);
            }
            infos[0].WeightSum.Content = "0";
        }

        /// <summary>
        /// Получение информации о вершине графа
        /// </summary>
        /// <param name="v">Вершина</param>
        /// <returns>Информация о вершине</returns>
        GraphVertexInfo GetVertexInfo(Vertex v)
        {
            foreach (var i in infos)
            {
                if (i.Vertex.Equals(v))
                {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// Поиск непосещенной вершины с минимальным значением суммы
        /// </summary>
        /// <returns>Информация о вершине</returns>
        public GraphVertexInfo FindUnvisitedVertexWithMinSum()
        {
            var minValue = int.MaxValue;
            GraphVertexInfo minVertexInfo = null;
            foreach (var i in infos)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }

        /// <summary>
        /// Поиск кратчайшего пути по названиям вершин
        /// </summary>
        /// <param name="startName">Название стартовой вершины</param>
        /// <param name="finishName">Название финишной вершины</param>
        /// <returns>Кратчайший путь</returns>
        public void FindShortestPath(string startName, string finishName)
        {
            startV = null;
            finishV = null;
            foreach (Vertex v in Vertices)
            {
                if (startName == v.getLabel())
                {
                    startV = v;
                }
                if (finishName == v.getLabel())
                {
                    finishV = v;
                }
            }
            if (startV != null && finishV != null)
            {
                FindShortestPath(startV, finishV);
            }
        }

        /// <summary>
        /// Поиск кратчайшего пути по вершинам
        /// </summary>
        /// <param name="startVertex">Стартовая вершина</param>
        /// <param name="finishVertex">Финишная вершина</param>
        /// <returns>Кратчайший путь</returns>
        public void FindShortestPath(Vertex startVertex, Vertex finishVertex)
        {
            InitInfo();
            var first = GetVertexInfo(startVertex);
            Logger.Add(new StringBuilder());
            Logger[Logger.Count - 1].Append("Начальная вершина: " + first.Vertex.getLabel());
            TBLogger.Text += Logger[Logger.Count - 1].ToString();
            first.EdgesWeightSum = 0;
            /* while (true)
             {
                 var current = FindUnvisitedVertexWithMinSum();
                 if (current == null)
                 {
                     Logger[Logger.Count - 1].Append("\n\nНепосещенных вершин больше нет");
                     break;
                 }
                 else
                 {
                  //   Logger.Add(new StringBuilder());
                     Logger[Logger.Count - 1].Append("\n\nСледущая минимальная вершина: " + current.Vertex.getLabel());
                     current.Relative.rectangle.Stroke = Brushes.Coral;
                 }

                 SetSumToNextVertex(current);
             }*/
            timer.Tick += Timer_Tick;
            timer.Start();

           // return GetPath(startVertex, finishVertex);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Done)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    Logger.Add(new StringBuilder());
                    Logger[Logger.Count - 1].Append("\n\nНепосещенных вершин больше нет");
                    Done = false;
                    
                }
                else
                {
                    //   Logger.Add(new StringBuilder());
                    Logger.Add(new StringBuilder());
                    Logger[Logger.Count - 1].Append("\n\nСледущая минимальная вершина: " + current.Vertex.getLabel());
                  
                    current.Relative.rectangle.Stroke = Brushes.Coral;
                    SetSumToNextVertex(current);
                   // TBLogger.Text += Logger[Logger.Count - 1].ToString();
                }

              //  SetSumToNextVertex(current);
                TBLogger.Text += Logger[Logger.Count - 1].ToString();
            }
            else
            {
                TBLogger.Text += "\nПройденный путь: " + GetPath(startV, finishV);
                timer.Stop();
            }
        }
        /// <summary>
        /// Вычисление суммы весов ребер для следующей вершины
        /// </summary>
        /// <param name="info">Информация о текущей вершине</param>
        void SetSumToNextVertex(GraphVertexInfo info)
        {
            info.IsUnvisited = false;
            foreach (var e in info.Vertex.getEdges())
            {
                var nextInfo = GetVertexInfo(e.Key);
                var sum = info.EdgesWeightSum + e.Value.getWeight();
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info.Vertex;
                    nextInfo.WeightSum.Content = sum;
                    Logger[Logger.Count - 1].Append("\nМинимальная сумма для вершины " + nextInfo.Vertex.getLabel() + " : " + sum);
                }
            }
        }

        /// <summary>
        /// Формирование пути
        /// </summary>
        /// <param name="startVertex">Начальная вершина</param>
        /// <param name="endVertex">Конечная вершина</param>
        /// <returns>Путь</returns>
        string GetPath(Vertex startVertex, Vertex endVertex)
        {
            StringBuilder path = new StringBuilder();

            //   var path = endVertex.getLabel();
            path.Append(endVertex.getLabel() + " ");
            result.Add(endVertex);
            while (startVertex != endVertex)
            {
                endVertex = GetVertexInfo(endVertex).PreviousVertex;
                //  path = endVertex.getLabel() + " " + path;
                path.Insert(0, endVertex.getLabel() + " ");
                result.Add(endVertex);
            }
            Draw();
            return path.ToString();
        }
        public void Draw()
        {
            TBLogger.Text += Logger[0];
            foreach (Relative r in Relatives)
            {
                if (r.vertex == result[result.Count-1])
                {
                    r.rectangle.Stroke = Brushes.Coral;
                    r.rectangle.Fill = Brushes.AliceBlue;
                }
            }
            for(int i = result.Count - 1; i > 0; i--)
            {
                foreach (Relative r in Relatives)
                {
                    if (r.vertex == result[i-1])
                    {
                      //  TBLogger.Text += Logger[i + 1];
                        r.rectangle.Stroke = Brushes.Coral;
                        r.rectangle.Fill = Brushes.AliceBlue;
                        Relative sr = null;
                        foreach (Relative re in Relatives)
                        {
                            if (re.vertex == result[i])
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

        }
        public void DoPanel()
        {
            DataPanel = new StackPanel()
            {
                Name = "DataPanel",
                Margin = new System.Windows.Thickness(600, 93, 10, 50),
            };
            Label StartV = new Label()
            {
                Content = "Start vertex:"
            };
            DataPanel.Children.Add(StartV);
            StartVBox = new TextBox()
            {
                Name = "Start_vertex",
            };
            DataPanel.Children.Add(StartVBox);
            Label FinishVData = new Label()
            {
                Content = "Finish vertex:"
            };
            DataPanel.Children.Add(FinishVData);
            FinishVBox = new TextBox()
            {
                Name = "Finish_vertex"
            };
            DataPanel.Children.Add(FinishVBox);

            BtnSave = new Button()
            {
                Content = "Save"
            };
            BtnSave.Click += BtnSave_Click;
            DataPanel.Children.Add(BtnSave);

            CnvBack.Children.Add(DataPanel);
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            CnvBack.Children.Remove(DataPanel);
            if(StartVBox.Text != null && StartVBox.Text != "" && FinishVBox.Text!= null && FinishVBox.Text != "")
            {
                CnvBack.Children.Remove(TBLogger);
                TBLogger.Text = "";
                CnvBack.Children.Add(TBLogger);
                FindShortestPath(StartVBox.Text, FinishVBox.Text);
            }
        }
    }

}
