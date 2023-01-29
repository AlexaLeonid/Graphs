using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class Prim_sAlgorithm
    {
        private List<Vertex> graph;
        StringBuilder sb = new StringBuilder();
        List<StringBuilder> Logger = new List<StringBuilder>();

        public Prim_sAlgorithm(List<Vertex> graph)
        {
            this.graph = graph;
        }

        public List<Vertex> getGraph()
        {
            return this.graph;
        }

        public void run()
        {
            //  FileHelper.Reset();
            Logger = new List<StringBuilder>();
            if (graph.Count() > 0)
            {
                graph[0].setVisited(true);
                sb.Append("Начальный элемент " + graph[0].getLabel());
                Logger.Add(new StringBuilder());
                Logger[Logger.Count - 1].Append("Начальный элемент " + graph[0].getLabel());
            }
            while (isDisconnected())
            {
                Edge nextMinimum = new Edge(Int32.MaxValue);
                Vertex nextVertex = graph[0];
    
                (Vertex, Edge) candidate = (null, null);
                foreach (Vertex vertex in graph)
                {
                    if (vertex.getVisited())
                    {
                        candidate = vertex.nextMinimum();
                        if (candidate.Item2.getWeight() < nextMinimum.getWeight())
                        {
                            nextMinimum = candidate.Item2;
                            nextVertex = candidate.Item1;
                            Logger.Add(new StringBuilder());
                        }
                    }
                }
                nextMinimum.setIncluded(true);
                nextVertex.setVisited(true);
                sb.Append("\nДобавлаем следующую вершину " + nextVertex.getLabel() + " с минимальным ребром " + nextMinimum.getWeight());
                Logger[Logger.Count - 1].Append("\nДобавлаем следующую вершину " + nextVertex.getLabel() + " с минимальным ребром " + nextMinimum.getWeight());
            }
            Logger.Add(new StringBuilder());
            sb.Append("\nОстовное дерево составлено!");
            Logger[Logger.Count - 1].Append("\nОстовное дерево составлено!");
         //   string path = "C:\\Users\\Пользователь\\Downloads\\labocha\\lab5\\LoggerSpanningTree.txt";
         //   File.WriteAllText(path, string.Empty);
         //   File.AppendAllText(path, sb.ToString());
        }
        bool isDisconnected()
        {
            foreach (Vertex vertex in graph)
            {
                if (!vertex.getVisited())
                {
                    return true;
                }
            }
            return false;
        }

        public String originalGraphToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Vertex vertex in graph)
            {
                sb.Append(vertex.originalToString());
            }
            return sb.ToString();
        }

        public void resetPrintHistory()
        {
            foreach (Vertex vertex in graph)
            {
                foreach (var pair in vertex.getEdges())
                {
                    pair.Value.setPrinted(false);
                }
            }
        }

        public string minimumSpanningTreeToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Vertex vertex in graph)
            {
                sb.Append(vertex.includedToString());
            }
            return sb.ToString();
        }
        public List<StringBuilder> minimumSpanningTree()
        {
            foreach (Vertex vertex in graph)
            {
                vertex.includedToTree();
            }
            return Logger;
        }
    }
}
