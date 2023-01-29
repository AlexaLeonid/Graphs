using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace lab5
{ 
    public class RectConnection
    {
        public Ellipse StartRect;
        public Ellipse FinishRect;
        public Polyline Line;
        public Label LPath = new Label() { Content = ", 0/0"};
        public Label WeightL;
    }
}
