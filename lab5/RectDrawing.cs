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
    internal class RectDrawing: Tool
    {
        static StringBuilder DataTxt;
        public static (Relative, Ellipse) Execute(Canvas CnvBack, Relative NewRelative, Ellipse NewRectangle, Vertex vertex, double x, double y)
        {
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

                NewRelative = new Relative()
                {
                    rectangle = NewRectangle,
                    vertex = vertex
                };
                Relatives.Add(NewRelative);
                //  People.Add(person);
                Canvas.SetZIndex(NewRelative.DataText, 6);
                CnvBack.Children.Add(NewRelative.DataText);
                //People.Add(person);
            }
            Canvas.SetTop(NewRectangle, y);
            Canvas.SetLeft(NewRectangle, x);
            DoHoverAffect(NewRectangle);
            EditPerson(NewRelative);
            Canvas.SetLeft(NewRelative.DataText, Canvas.GetLeft(NewRelative.rectangle)+16);
            Canvas.SetTop(NewRelative.DataText, Canvas.GetTop(NewRelative.rectangle)+12.5);


            return (NewRelative, NewRectangle);
        }
        private static void EditPerson(Relative relative)
        {
            Vertex vertex = relative.vertex;
            DataTxt = new StringBuilder();
            if (vertex.getLabel() != null && vertex.getLabel() != "")
            {
                DataTxt.Append(vertex.getLabel());
                //   relative.person.Name = NameBox.Text;
            }
            
            relative.DataText.Content = DataTxt.ToString();
            // ShowData(relative, DataText.ToString());       // EditPeople();

        }

    }
}
