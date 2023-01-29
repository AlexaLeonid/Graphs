using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace lab5
{
    class MaxFlow: Tool
    {
        Canvas CnvBack;
        DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        List<StringBuilder> Logger = new List<StringBuilder>();
        int i = 0;
        public MaxFlow(Canvas cnvBack)
        {
            this.CnvBack = cnvBack;
            //Check();
            FileHelper.Reset(cnvBack);
            CnvBack.Children.Remove(TBLogger);
            TBLogger.Text = "";
            CnvBack.Children.Add(TBLogger);
            Execute();
        }
        /*
        void Check()
        {
            foreach (Vertex v in Vertices)
            {
                if (v.getLabel() == null || v.getLabel() == "")
                {
                    //
                    break;
                }
            }
        }
        */


        void Execute()
        {
            FlowAlgorithm flow = new FlowAlgorithm(CnvBack);
            flow.run();


           /* foreach (Relative r in Relatives)
            {
                if (r.vertex == spanTree[0].Item1)
                {
                    r.rectangle.Stroke = Brushes.Coral;
                    r.rectangle.Fill = Brushes.AliceBlue;
                }
            }
            TBLogger.Text += Logger[0].ToString();
            timer.Tick += Timer_Tick;
            timer.Start();*/
            /* for (int i = 0; i < spanTree.Count; i++)
             {
                 foreach (Relative r in Relatives)
                 {
                     if(r.vertex == spanTree[i].Item2)
                     {
                         r.rectangle.Stroke = Brushes.Coral;
                         r.rectangle.Fill = Brushes.AliceBlue;
                         // Thread.Sleep(100);
                         Relative sr = null;
                         foreach (Relative re in Relatives)
                         {
                             if(re.vertex == spanTree[i].Item1)
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
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (i < spanTree.Count)
            {

                foreach (Relative r in Relatives)
                {
                    if (r.vertex == spanTree[i].Item2)
                    {
                        TBLogger.Text += Logger[i + 1];
                        r.rectangle.Stroke = Brushes.Coral;
                        r.rectangle.Fill = Brushes.AliceBlue;
                        // Thread.Sleep(100);
                        Relative sr = null;
                        foreach (Relative re in Relatives)
                        {
                            if (re.vertex == spanTree[i].Item1)
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
            }
            else
            {
                timer.Stop();
            }

            i++;
        }

    }
}
