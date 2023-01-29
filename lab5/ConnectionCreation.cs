using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace lab5
{
    public class ConnectionCreation : Tool
    {
        Canvas CnvBack;

        public Point IsMouseDouwLocation;
  
        public bool IsMouseDown = false;
        private double StartX;
        private double StartY;
        private double CurX;
        private double CurY;
        Point StartDownLocation;
        private Polyline NewLine;
        PointCollection MyPointCollection;
        Ellipse SoursRect;
        public Ellipse ClickedRectangle;
        public ConnectionCreation(Canvas CnvBack)
        {
          //  Label l = new Label() { Content = "ЛЯЛЯ" };
         //   CnvBack.Children.Add(l);

          //  Canvas.SetTop(l, 100);
         //   Canvas.SetLeft(l, 100);
            this.CnvBack = CnvBack;
            CnvBack.Children.Remove(TBLogger);
            DoLine = true;
            DoNewConnection();
        }
        public void DoNewConnection()
        {
            CnvBack.MouseDown += CnvBack_LineMouseDown;
            CnvBack.MouseMove += DrawLineMouseMove;
            CnvBack.MouseUp += CnvBack_LineMouseUp;
        }
        private void CnvBack_LineMouseDown(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = true;
            if (e.OriginalSource is Ellipse)
            {
                ClickedRectangle = (Ellipse)e.OriginalSource;
                SoursRect = (Ellipse)e.OriginalSource;
                StartX = e.GetPosition(CnvBack).X;
                StartY = e.GetPosition(CnvBack).Y;
                CurX = e.GetPosition(CnvBack).X;
                CurY = e.GetPosition(CnvBack).Y;
                StartDownLocation = new Point(StartX, StartY);
            }
        }
        private void DrawLineMouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == false)
            {
                return;
            }
            CurX = e.GetPosition(CnvBack).X;
            CurY = e.GetPosition(CnvBack).Y;

            if (NewLine == null)
            {
                NewLine = new Polyline
                {
                    Stroke = Brushes.Green,
                    StrokeThickness = 2
                };

                Canvas.SetZIndex(NewLine, 1);
                CnvBack.Children.Add(NewLine);
            }

            MyPointCollection = new PointCollection();
            MyPointCollection.Add(StartDownLocation);
            Point CurLocation = new Point(CurX, CurY);
            MyPointCollection.Add(CurLocation);
            NewLine.Points = MyPointCollection;
            DoHoverAffect(NewLine);
            CnvBack.InvalidateVisual();
        }
        private void DrawLineBetweenRects(double mouseX, double mouseY)
        {
            MyPointCollection = new PointCollection();
            MyPointCollection.Add(StartDownLocation);
            Point CurLocation = new Point(mouseX, mouseY);
            MyPointCollection.Add(CurLocation);
            NewLine.Points = MyPointCollection;
            ClickedRectangle = null;
        }
        private void CnvBack_LineMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DoLine == true)
            {
                if (e.OriginalSource is Ellipse)
                {
                    DropHoverAffect(NewLine);
                    double mouseX = e.GetPosition(CnvBack).X;
                    double mouseY = e.GetPosition(CnvBack).Y;
                    DrawLineBetweenRects(mouseX, mouseY);
                    ConnectionsInfo.Add(new RectConnection
                    {
                        StartRect = SoursRect,
                        FinishRect = (Ellipse)e.OriginalSource,
                        Line = NewLine,
                        WeightL = new Label(),
                        LPath = new Label()
                    });
                    Canvas.SetZIndex(ConnectionsInfo[ConnectionsInfo.Count-1].WeightL, 6);
                    CnvBack.Children.Add(ConnectionsInfo[ConnectionsInfo.Count - 1].WeightL);
                    Canvas.SetTop(ConnectionsInfo[ConnectionsInfo.Count - 1].WeightL, (StartY + CurY) / 2);
                    Canvas.SetLeft(ConnectionsInfo[ConnectionsInfo.Count - 1].WeightL, (StartX + CurX) / 2);
                }
                else
                { 
                    NewLine.Points = null;
                }
            }
            NewLine = null;
            IsMouseDown = false;
            DoLine = false;
            CnvBack.MouseDown -= CnvBack_LineMouseDown;
            CnvBack.MouseMove -= DrawLineMouseMove;
            CnvBack.MouseUp -= CnvBack_LineMouseUp;           
        }
    }
}
