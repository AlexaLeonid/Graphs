using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace lab5
{
    public class DataAddition: Tool
    {
        Canvas CnvBack;
        //   private Person NewPerson;
        private StringBuilder DataText;
        private TextBox NameBox;
        private TextBox SVertexBox;
        private TextBox WeightBox;
        private TextBox LPathBox = new TextBox();
        private Button BtnSave;
        private StackPanel DataPanel;
        private Ellipse ChosenRect;
        private Relative ChosenRel;
        public DataAddition(Canvas CnvBack)
        {
            this.CnvBack = CnvBack;
            CnvBack.Children.Remove(TBLogger);
            EditData = true;
            NewPers();
        }
        public void NewPers()
        {
            EditData = true;
            CnvBack.MouseLeftButtonDown += CnvBack_MouseLeftButtonDown;
            CnvBack.MouseLeftButtonUp += CnvBack_MouseLeftButtonUp;
        }
        private void DoDataBoxes()
        {
            NameBox = new TextBox()
            {
                Name = "NameTxt"
            };
            SVertexBox = new TextBox()
            {
                Name = "SVertexTxt"
            };
            WeightBox = new TextBox()
            {
                Name = "WeightTxt"
            };
            LPathBox = new TextBox()
            {
                Name = "PathTxt"
            };

        }
        private void CnvBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Ellipse)
            {
                ChosenRect = (Ellipse)e.OriginalSource;
                ChosenRel = FindRelative(ChosenRect);
                DataPanel = new StackPanel()
                {
                    Name = "DataPanel",
                    Margin = new System.Windows.Thickness(600, 93, 10, 50),
                };
                Label NameData = new Label()
                {
                    Content = "_Name:"
                };
                DataPanel.Children.Add(NameData);
                NameBox = new TextBox()
                {
                    Name = "Name",
                };
                DataPanel.Children.Add(NameBox);
                Label sVertexData = new Label()
                {
                    Content = "Vertex:"
                };
                DataPanel.Children.Add(sVertexData);
                SVertexBox = new TextBox()
                {
                    Name = "Birth"
                };
                DataPanel.Children.Add(SVertexBox);
                Label WeightData = new Label()
                {
                    Content = "Weight:"
                };
                DataPanel.Children.Add(WeightData);
                WeightBox = new TextBox()
                {
                    Name = "Weight"
                };
                DataPanel.Children.Add(WeightBox);
              /*  LPathBox = new TextBox()
                {
                    Name = "Path(W)"
                };
                DataPanel.Children.Add(LPathBox);*/

                BtnSave = new Button()
                {
                    Content = "Save"
                };
                ShowPersonData();
                BtnSave.Click += BtnSave_Click;
                DataPanel.Children.Add(BtnSave);

                CnvBack.Children.Add(DataPanel);
            };
        }
        private void ShowPersonData()
        {
            Vertex chosenPers = ChosenRel.vertex;
            if (chosenPers.getLabel() != null && chosenPers.getLabel() != "")
            {
                NameBox.Text = chosenPers.getLabel();
            }
            
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            EditPerson(ChosenRel);
            CnvBack.Children.Remove(DataPanel);
        }
        private Relative FindRelative(Ellipse rect)
        {
            foreach (Relative relative in Relatives)
            {
                if (ChosenRect == relative.rectangle)
                {
                    return relative;
                }
            }
            return null;
        }
        private void EditPerson(Relative relative)
        {
            DataText = new StringBuilder();
            if (NameBox.Text != null && NameBox.Text != "")
            {
                DataText.Append(NameBox.Text);
                relative.vertex.setLabel(NameBox.Text);
            }
            if (SVertexBox.Text != null && SVertexBox.Text != "" && WeightBox.Text != null && WeightBox.Text != "")
            {
                foreach(Relative r in Relatives)
                {
                    if (r.DataText.Content.ToString() == SVertexBox.Text.ToString())
                    {
                        char[] pair = (ChosenRel.vertex.getLabel() + r.vertex.getLabel()).ToCharArray();
                        Array.Sort(pair);
                        string key = new string(pair);
                        int n;
                        if (edges.ContainsKey(key) && Int32.TryParse(WeightBox.Text, out n))
                        {
                            if (edges[key].getWeight() != n)
                            {
                                edges[key].setWeight(n);
                                r.vertex.getEdges()[ChosenRel.vertex].setWeight(n);
                                ChosenRel.vertex.getEdges()[r.vertex].setWeight(n);
                            }                           
                        }
                        else
                        {
                            if (Int32.TryParse(WeightBox.Text, out n))
                            {
                                edges.Add(key, new Edge(n));
                                r.vertex.getEdges().Add(ChosenRel.vertex, new Edge(n));
                                ChosenRel.vertex.getEdges().Add(r.vertex, new Edge(n));
                            }
                        }
                        UpdateConnetionWeight(r, ChosenRel, n);
                    }
                }
            }
            //if (LPathBox.Text != null && LPathBox.Text != "")
            {

               /* foreach (Relative r in Relatives)
                {
                    if (r.DataText.Content.ToString() == SVertexBox.Text.ToString())
                    {
                        char[] pair = (ChosenRel.vertex.getLabel() + r.vertex.getLabel()).ToCharArray();
                        Array.Sort(pair);
                        string key = new string(pair);
                        int n;
                        if (edges.ContainsKey(key) && Int32.TryParse(WeightBox.Text, out n))
                        {
                            if (edges[key].getWeight() != n)
                            {
                                edges[key].setWeight(n);
                                r.vertex.getEdges()[ChosenRel.vertex].setWeight(n);
                                ChosenRel.vertex.getEdges()[r.vertex].setWeight(n);
                            }
                        }
                        else
                        {
                            if (Int32.TryParse(WeightBox.Text, out n))
                            {
                                edges.Add(key, new Edge(n));
                                r.vertex.getEdges().Add(ChosenRel.vertex, new Edge(n));
                                ChosenRel.vertex.getEdges().Add(r.vertex, new Edge(n));
                            }
                        }
                        UpdateConnetionWeight(r, ChosenRel, n);
                    }
                }*/
            }
            ShowData(relative, DataText.ToString());
            bool there = false;
            foreach(Vertex v in Vertices)
            {
                if(v == ChosenRel.vertex)
                {
                    there = true;
                }
            }
            if(!there)
            {
                Vertices.Add(ChosenRel.vertex);
            }
        }
        public void UpdateConnetionWeight(Relative fR, Relative sR, int n)
        {
            foreach (RectConnection connection in ConnectionsInfo)
            {
                if ((connection.StartRect == fR.rectangle && connection.FinishRect == sR.rectangle) || (connection.FinishRect == fR.rectangle && connection.StartRect == sR.rectangle))
                { 
                    connection.WeightL.Content = n.ToString();
                }
            }

        }
        private void ShowData(Relative relative, string dataText)
        {
            relative.DataText.Content = dataText;           
            //   CnvBack.Children.Add(relative.DataText);
        }

        private void CnvBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CnvBack.MouseLeftButtonDown -= CnvBack_MouseLeftButtonDown;
            CnvBack.MouseLeftButtonUp -= CnvBack_MouseLeftButtonUp;
        }
    }

}

