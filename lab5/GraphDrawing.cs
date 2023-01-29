using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace lab5
{
    internal class GraphDrawing: Tool
    {
        Canvas CnvBack;
        Ellipse NewRectangle;
        int n1;
        int n2;
        int n3;
        int n4;
        double startX = 100;
        double startY = 100;
        double fiX;
        double fiY;
        double sX;
        double sY;
        double tX;
        double tY;
        double foX;
        double foY;
        public GraphDrawing(Canvas CnvBack)
        {
            this.CnvBack = CnvBack;
            DrawTree();
        }
        private void DrawTree()
        {
            if (Vertices != null && Vertices.Count > 0)
            {
                if (Vertices.Count % 4 == 0)
                {
                    n1 = Vertices.Count / 4;
                    n2 = Vertices.Count / 4;
                    n3 = Vertices.Count / 4;
                    n4 = Vertices.Count / 4;
                }
                else if(Vertices.Count % 4 == 1)
                {
                    n1 = Vertices.Count / 4 + 1;
                    n2 = Vertices.Count / 4;
                    n3 = Vertices.Count / 4;
                    n4 = Vertices.Count / 4;
                }
                else if (Vertices.Count % 4 == 2)
                {
                    n1 = Vertices.Count / 4 + 1;
                    n2 = Vertices.Count / 4 + 1;
                    n3 = Vertices.Count / 4;
                    n4 = Vertices.Count / 4;
                }
                else if (Vertices.Count % 4 == 3)
                {
                    n1 = Vertices.Count / 4 + 1;
                    n2 = Vertices.Count / 4 + 1;
                    n3 = Vertices.Count / 4 + 1;
                    n4 = Vertices.Count / 4;
                }
                int i = 0;
                for (int k = 0; k < n1; k++)
                {
                    fiX = startX + 100;
                    fiY = startY;
                    (Relative NewRelative, Ellipse NewRectangle) = RectDrawing.Execute(CnvBack, null, null, Vertices[i], fiX + k*125, fiY);
                    DropHoverAffect(NewRectangle);
                    i++;
                }
                for (int k = 0; k < n2; k++)
                {
                    sX = startX + 125 * n1 + 100;
                    sY = startY + 100;
                    (Relative NewRelative, Ellipse NewRectangle) = RectDrawing.Execute(CnvBack, null, null, Vertices[i], sX, sY + 125*k);
                    DropHoverAffect(NewRectangle);
                    i++;

                }
                for (int k = 0; k < n3; k++)
                {
                    tX = startX + 100;
                    tY = startY + 125 * n2 + 100;
                    (Relative NewRelative, Ellipse NewRectangle) = RectDrawing.Execute(CnvBack, null, null, Vertices[i], tX + k * 125, tY);
                    DropHoverAffect(NewRectangle);
                    i++;
                }
                for (int k = 0; k < n4; k++)
                {
                    foX = startX;
                    foY = startY + 100;
                    (Relative NewRelative, Ellipse NewRectangle) = RectDrawing.Execute(CnvBack, null, null, Vertices[i], foX, foY + 125*k);
                    DropHoverAffect(NewRectangle);
                    sX = fiX + k * 100;
                    sY = fiY;
                    i++;
                }
            }
            DrawNewLines();
        }

        public void DrawNewLines()
        {
            int n = edges.Count;
            foreach (var pair in edges)
            {
                foreach (Relative r1 in Relatives)
                {
                    if (pair.Key[0].ToString() == r1.DataText.Content.ToString())
                    {
                        foreach(Relative r2 in Relatives)
                        {
                            if (pair.Key[1].ToString() == r2.DataText.Content.ToString())
                            {
                                ConnectionsInfo.Add(new RectConnection()
                                {
                                    StartRect = r1.rectangle,
                                    FinishRect = r2.rectangle,
                                    WeightL = new Label()
                                    { 
                                        Content = pair.Value.getWeight().ToString()
                                    },
                                    Line = new Polyline()
                                    {
                                        Stroke = Brushes.Green,
                                        StrokeThickness = 2
                                    },
                                });
                                Canvas.SetZIndex(ConnectionsInfo[ConnectionsInfo.Count - 1].Line, 1);
                                Canvas.SetZIndex(ConnectionsInfo[ConnectionsInfo.Count - 1].WeightL, 1);
                                CnvBack.Children.Add(ConnectionsInfo[ConnectionsInfo.Count - 1].Line);
                                CnvBack.Children.Add(ConnectionsInfo[ConnectionsInfo.Count - 1].WeightL);
                                UpdateConnection(ConnectionsInfo[ConnectionsInfo.Count - 1]);
                               // Label l = new Label() { Content = "ЛЯЛЯ" };
                              //  CnvBack.Children.Add(l);

                              //  Canvas.SetTop(l, 100);
                              //  Canvas.SetLeft(l, 100);
                            }
                        }
                    }
                }             
            }
        }

        /* private void DrawRelative(Person person)
         {
             if (NewRectangle == null)
             {
                 NewRectangle = new Rectangle()
                 {
                     Width = 70,
                     Height = 70,
                     Stroke = Brushes.Gray,
                     Fill = Brushes.Bisque,
                     StrokeThickness = 3
                 };

                 CnvBack.Children.Add(NewRectangle);
                 Canvas.SetZIndex(NewRectangle, 5);

                 NewRelative = new Relative()
                 {
                     rectangle = NewRectangle,
                     person = new Person()
                     {
                         Id = Relatives.Count
                     }
                 };
                 Relatives.Add(NewRelative);
                 Canvas.SetZIndex(NewRelative.DataText, 6);
                 CnvBack.Children.Add(NewRelative.DataText);
             }
             Canvas.SetTop(NewRectangle, person.CoordinatesY);
             Canvas.SetLeft(NewRectangle, person.CoordinatesX);
             DoHoverAffect(NewRectangle);
             Canvas.SetLeft(NewRelative.DataText, Canvas.GetLeft(NewRelative.rectangle));
             Canvas.SetTop(NewRelative.DataText, Canvas.GetTop(NewRelative.rectangle));
         }*/


    }
}
