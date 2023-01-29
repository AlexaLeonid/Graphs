using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace lab5
{
    public class RectCreation : Tool
    {
        Canvas CnvBack;
        Ellipse NewRectangle;
        Relative NewRelative;
        //    private List<Rectangle> Rectangles = new List<Rectangle>();

        public RectCreation(Canvas CnvBack)
        {
            this.CnvBack = CnvBack;
            CnvBack.Children.Remove(TBLogger);
            NewRect();
        }
        public void NewRect()
        {
            CnvBack.MouseMove += DrawRectangleMove;
            CnvBack.MouseDown += RectMouseDown;
        }
        private void RectMouseDown(object sender, MouseButtonEventArgs e)
        {
            CnvBack.MouseMove -= DrawRectangleMove;
            CnvBack.MouseDown -= RectMouseDown;
           // DropHoverAffect(NewRectangle);
            DropHoverAffect(NewRelative.rectangle);
            NewRectangle = null;
        }
        public void DrawRectangleMove(object sender, MouseEventArgs e)
        {
            double mouseX = e.GetPosition(CnvBack).X;
            double mouseY = e.GetPosition(CnvBack).Y;

            if (NewRectangle == null)
            {
                NewRectangle = new Ellipse()
                {
                    Width = 50,
                    Height = 50,
                    Stroke = Brushes.Gray,
                    Fill = Brushes.Bisque,
                    StrokeThickness = 3
                };
                CnvBack.Children.Add(NewRectangle);
                Canvas.SetZIndex(NewRectangle, 5);
                //             Rectangles.Add(NewRectangle);
                NewRelative = new Relative()
                {
                    rectangle = NewRectangle,
                    vertex = new Vertex()
                    {
                        // Id = Relatives.Count
                    }
                };
                Relatives.Add(NewRelative);
                //  Canvas.SetLeft(NewRelative.DataText, Canvas.GetLeft(NewRelative.rectangle));
                //  Canvas.SetTop(NewRelative.DataText, Canvas.GetTop(NewRelative.rectangle));
                Canvas.SetZIndex(NewRelative.DataText, 6);
                CnvBack.Children.Add(NewRelative.DataText);

            }
            Canvas.SetTop(NewRectangle, mouseY);
            Canvas.SetLeft(NewRectangle, mouseX);
            DoHoverAffect(NewRectangle);
            Canvas.SetLeft(NewRelative.DataText, Canvas.GetLeft(NewRelative.rectangle)+16);
            Canvas.SetTop(NewRelative.DataText, Canvas.GetTop(NewRelative.rectangle)+12.5);
            // LblCount.Content = Rectangles.Count;
        }
    }
}
