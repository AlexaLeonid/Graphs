using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace lab5
{
    internal class Deletion: Tool
    {
        Canvas CnvBack;
        Ellipse ClickedEllipse;
        public Deletion(Canvas CnvBack)
        {
            this.CnvBack = CnvBack;
            CnvBack.Children.Remove(TBLogger);
            Execute();
        }

        public void Execute()
        {
            CnvBack.MouseDown += RectMouseDown;
        }
        private void RectMouseDown(object sender, MouseButtonEventArgs e)
        {
            CnvBack.MouseDown -= RectMouseDown;
            // DropHoverAffect(NewRectangle);
            List<RectConnection> l = new List<RectConnection>();
            if (e.OriginalSource is Ellipse)
            {
                ClickedEllipse = (Ellipse)e.OriginalSource;
                foreach (RectConnection c in ConnectionsInfo)
                {
                    if (c.StartRect != ClickedEllipse && c.FinishRect != ClickedEllipse)
                    {
                        l.Add(c);
                    }
                    else
                    {
                        CnvBack.Children.Remove(c.Line);
                        CnvBack.Children.Remove(c.WeightL);
                        List<Relative> rs = new List<Relative>();
                        List<Vertex> vs = new List<Vertex>();
                        foreach (Relative r in Relatives)
                        {
                            if(r.rectangle == ClickedEllipse)
                            {
                                CnvBack.Children.Remove(r.DataText);
                            }
                            else
                            {
                                rs.Add(r);
                                vs.Add(r.vertex);
                            }
                        }
                        Relatives = rs;
                        Vertices = vs;
                    }
                }
                ConnectionsInfo = l;
                CnvBack.Children.Remove(ClickedEllipse);
            }
          //  CnvBack.MouseDown -= RectMouseDown;
        }
    }
}
