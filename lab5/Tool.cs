using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace lab5
{
    public abstract class Tool
    {
        public static List<RectConnection> ConnectionsInfo = new List<RectConnection>();
        public static List<Relative> Relatives = new List<Relative>();
        public static List<Vertex> Vertices = new List<Vertex>();
        public static List<(Vertex, Vertex)> spanTree = new List<(Vertex, Vertex)>();
        public static Dictionary<string, Edge> edges = new Dictionary<string, Edge>();
        public static List<GraphVertexInfo> infos = new List<GraphVertexInfo>();
        public static TextBox TBLogger = new TextBox()
        {
            Name = "TBLogger",
          //  Text = "KZCKZCN:S:CKCMD?LKMF:DKFMD:LFKM::LDKFN:LDVNSKJVNSKVj",
            Margin = new Thickness(550, 15, 500, 500)
        };
        public static bool DoLine = false;
        public static bool EditData = false;
        public static void DoHoverAffect(Shape shape)
        {
            if (shape != null)
            {
                shape.Effect = new DropShadowEffect
                {
                    Color = Colors.Orange,
                    BlurRadius = 50,
                    Direction = 0,
                    ShadowDepth = 0
                };
            }
        }
        public static void DropHoverAffect(Shape shape)
        {
            if(shape != null)
            {
                shape.Effect = null;
            }
        }
        public void UpdateConnection(RectConnection connection)
        {
            Point startLocation;
            Point finishLocation;
            PointCollection MyPointCollection = new PointCollection();
            double startX = Canvas.GetLeft(connection.StartRect);
            double startY = Canvas.GetTop(connection.StartRect);
            double finishX = Canvas.GetLeft(connection.FinishRect);
            double finishY = Canvas.GetTop(connection.FinishRect);
            startLocation = new Point(startX + 25, startY + 25);
            finishLocation = new Point(finishX + 25, finishY + 25);
            
            MyPointCollection.Add(startLocation);
            MyPointCollection.Add(finishLocation);
            connection.Line.Points = MyPointCollection;

            Canvas.SetTop(connection.WeightL, (startY + finishY) / 2);
            Canvas.SetLeft(connection.WeightL, (startX + finishX) / 2);
            //  RightConnection = connection;
            //  DoHoverAffect(сonnection.Line);
        }
    }
}
