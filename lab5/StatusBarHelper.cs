using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace lab5
{
    public class StatusBarHelper
    {
        private Label LblCoordinates;

        public StatusBarHelper(Label LblCoordinates)
        {
            this.LblCoordinates = LblCoordinates;

        }
        public void UpDateCoordinates(Point? point)
        {
            if (point != null)
            {
                LblCoordinates.Content = $"X:{Math.Round(point.Value.X)} Y:{Math.Round(point.Value.Y)}";
            }
        }

    }
}
