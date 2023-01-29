using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace lab5
{
    public class Vertex: Tool
    {
      
        private string label;
        private Dictionary<Vertex, Edge> edges = new Dictionary<Vertex, Edge>();

        private bool isVisited = false;
        public int EdgesWeightSum;
        public Vertex(string label)
        {
            this.label = label;
        }
        public Vertex()
        {
        }
        public void setVisited(bool a)
        {
            this.isVisited = a;
        }
        public void setLabel(string a)
        {
            this.label = a;
        }
        public bool getVisited()
        {
            return isVisited;
        }
        public void addEdge(Vertex v, Edge e)
        {
            edges.Add(v, e);
        }

        public string getLabel()
        {
            return label;
        }

        public Dictionary<Vertex, Edge> getEdges()
        {
            return edges;
        }
        public (Vertex, Edge) nextMinimum()
        {
            Edge nextMinimum = new Edge(Int32.MaxValue);
            Vertex nextVertex = this;

            foreach (var pair in edges)
            {
                if (!pair.Key.getVisited())
                {
                    if (!pair.Value.getIncluded())
                    {
                        if (pair.Value.getWeight() < nextMinimum.getWeight())
                        {
                            nextMinimum = pair.Value;
                            nextVertex = pair.Key;
                        }
                    }
                }
            }
            return (nextVertex, nextMinimum);
        }
        public String originalToString()
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (var pair in edges)
            {
                if (!pair.Value.getPrinted())
                {
                    sb.Append(getLabel());
                    sb.Append(" --- ");
                    sb.Append(pair.Value.getWeight());
                    sb.Append(" --- ");
                    sb.Append(pair.Key.getLabel());
                    sb.Append("\n");
                    pair.Value.setPrinted(true);
                }
            }
            return sb.ToString();
        }

        public String includedToString()
        {
            StringBuilder sb = new StringBuilder();
            if (getVisited())
            {
                foreach (var pair in edges)
                {
                    if (pair.Value.getIncluded())
                    {
                        if (!pair.Value.getPrinted())
                        {
                              sb.Append(getLabel());
                              sb.Append(" --- ");
                              sb.Append(pair.Value.getWeight());
                              sb.Append(" --- ");
                              sb.Append(pair.Key.getLabel());
                              sb.Append("\n");
                              pair.Value.setPrinted(true);
                        }
                    }
                }
            }
            
            return sb.ToString();
        }
        public void includedToTree()
        {
            if (getVisited())
            {
                foreach (var pair in edges)
                {
                    if (pair.Value.getIncluded())
                    {
                        if (!pair.Value.getPrinted())
                        {
                            spanTree.Add((this, pair.Key));
                        }
                    }
                }
            }
        }
    }
}
