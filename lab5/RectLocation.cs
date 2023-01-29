using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace lab5
{
    class RectLocation : Tool
    {
        bool HoverLine = false;
        Canvas CnvBack;
        StatusBarHelper statusBarHelper;
        bool IsPressed = false;
        Ellipse ClickedRectangle;
        RectConnection RightConnection;
        public RectLocation(Canvas CnvBack, StatusBarHelper statusBarHelper)
        {
            this.statusBarHelper = statusBarHelper;
            this.CnvBack = CnvBack;
            RectMove();
        }
        public void RectMove()
        {
            CnvBack.MouseLeftButtonDown += CnvBack_MouseLeftButtonDown;
            CnvBack.MouseLeftButtonUp += CnvBack_MouseLeftButtonUp;
            CnvBack.MouseMove += CnvBack_MouseMove;
        }

        private void CnvBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Ellipse && DoLine == false)
            {
                IsPressed = true;
                ClickedRectangle = (Ellipse)e.OriginalSource;
                DoHoverAffect(ClickedRectangle);
                ClickedRectangle.CaptureMouse();
            }
        }
        private void CnvBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (e.OriginalSource is Ellipse && DoLine == false)
            {
                if (HoverLine == true)
                {
                    DropHoverAffect(RightConnection.Line);
                    HoverLine = false;
                }
                DropHoverAffect(ClickedRectangle);
                ClickedRectangle.ReleaseMouseCapture();
                ClickedRectangle = null;
                IsPressed = false;
            }
        }
        private void CnvBack_MouseMove(object sender, MouseEventArgs e)
        {
            Point Cursor = Mouse.GetPosition(CnvBack);
            statusBarHelper.UpDateCoordinates(Cursor);
            if (e.OriginalSource is Ellipse && DoLine == false)
            {
                if (IsPressed == true)
                {
                    double mouseX = e.GetPosition(CnvBack).X;
                    double mouseY = e.GetPosition(CnvBack).Y;
                    Canvas.SetLeft(ClickedRectangle, mouseX);
                    Canvas.SetTop(ClickedRectangle, mouseY);
                    Relative ClickedRelative = FindRelative((Ellipse)e.OriginalSource);
                    if (ClickedRelative != null)
                    {
                        Canvas.SetLeft(ClickedRelative.DataText, mouseX+16);
                        Canvas.SetTop(ClickedRelative.DataText, mouseY+12.5);
                    }

                    foreach (RectConnection connection in ConnectionsInfo)
                    {
                        if (connection.StartRect == (Ellipse)e.OriginalSource || connection.FinishRect == (Ellipse)e.OriginalSource)
                        {
                            HoverLine = true;         
                            UpdateConnection(connection);
                        }
                    }
                }
            }
        }
        private Relative FindRelative(Ellipse rect)
        {
            foreach (Relative relative in Relatives)
            {
                if (relative.rectangle == rect)
                {
                    return relative;
                }
            }
            return null;
        }

        private void UpdateConnection(RectConnection connection)
        {
            PointCollection MyPointCollection = new PointCollection();
            double startX = Canvas.GetLeft(connection.StartRect) + 25;
            double startY = Canvas.GetTop(connection.StartRect) + 25;
            Point startLocation = new Point(startX, startY);
            double finishX = Canvas.GetLeft(connection.FinishRect) +25;
            double finishY = Canvas.GetTop(connection.FinishRect) + 25;
            Point finishLocation = new Point(finishX, finishY);
            MyPointCollection.Add(startLocation);
            MyPointCollection.Add(finishLocation);
            connection.Line.Points = MyPointCollection;
            RightConnection = connection;
            DoHoverAffect(RightConnection.Line);

            Canvas.SetTop(connection.WeightL, (startY+finishY)/2);
            Canvas.SetLeft(connection.WeightL, (startX + finishX) / 2);
        }
    }
}
