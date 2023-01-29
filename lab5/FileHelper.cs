using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace lab5
{
    public class FileHelper: Tool
    {
        public static void Reset(Canvas CnvBack)
        {
            if(infos != null && infos.Count > 0)
            {
                foreach(GraphVertexInfo v in infos)
                {
                    CnvBack.Children.Remove(v.WeightSum);
                }
            }
            spanTree.Clear();
            foreach(Relative r in Relatives)
            {
                r.rectangle.Stroke = Brushes.Gray;
                r.rectangle.Fill = Brushes.Bisque;
                r.vertex.setVisited(false);
            }
            foreach (var e in edges)
            {
                e.Value.setPrinted(false);
            }
            foreach (RectConnection rc in ConnectionsInfo)
            {
                rc.Line.Stroke = Brushes.Green;
                CnvBack.Children.Remove(rc.LPath);
            }
        }
        public static List<Vertex> ReadGraph(string path)
        {
            string[] lines = File.ReadAllLines(path);
            List<Vertex> vertices = new List<Vertex>();

            for (int i = 0; i < lines.Length - 1; i++)
            {
                vertices.Add(new Vertex(lines[i + 1][0].ToString()));
            }
          /* foreach (Vertex v in vertices)
            {
                Console.WriteLine(v.getLabel());
            }*/
         //   Dictionary<string, Edge> edges = new Dictionary<string, Edge>();

            for (int i = 0; i < lines.Length - 1; i++)
            {
                for (int j = 0; j < lines[0].Length - 1; j++)
                {
                    int n;
                    if (int.TryParse(lines[i + 1][j + 1].ToString(), out n))
                    {
                        char[] pair = new char[2] { lines[i + 1][0], lines[0][j + 1] };
                        Array.Sort(pair);
                        if (n > 0 && !edges.ContainsKey(new string(pair)))
                        {
                            edges.Add(new string(pair), new Edge(n));
                        }
                    }
                }
            }

          /*  foreach (var v in edges)
            {
                Console.WriteLine(v.Key[0].ToString() + v.Key[1].ToString() + "       " + v.Value.getWeight());
            }*/

            for (int i = 0; i < vertices.Count - 1; i++)
            {
                for (int j = i; j < vertices.Count; j++)
                {
                    char[] pair = (vertices[i].getLabel() + vertices[j].getLabel()).ToCharArray();
                    Array.Sort(pair);
                    string key = new string(pair);
                    if (edges.ContainsKey(key))
                    {
                        vertices[i].addEdge(vertices[j], edges[key]);
                        vertices[j].addEdge(vertices[i], edges[key]);
                    }
                }
            }
            return vertices;
        }

        public static void SaveGraph(List<Vertex> graph)
        {
            List<String> lines = new List<string>();

            StringBuilder sb = new StringBuilder();
            sb.Append(" ");
            for (int i = 0; i < graph.Count; i++)
            {
                sb.Append(graph[i].getLabel());
            }
            lines.Add(sb.ToString());
 
            foreach (Vertex v1 in graph)
            {
                sb = new StringBuilder();
                sb.Append(v1.getLabel());
                foreach (Vertex v2 in graph)
                {
                    if (v1.getEdges().ContainsKey(v2))
                    {
                        sb.Append(v1.getEdges()[v2].getWeight());
                    }
                    else
                    {
                        sb.Append("0");
                    }
                }
                lines.Add(sb.ToString());
            }
            WriteFile(lines);
        }
        public static void WriteFile(List<String> lines)
        {
            string path = "C:\\Users\\Пользователь\\Downloads\\lab5\\lab5\\testWrite.txt";
            File.WriteAllText(path, string.Empty);
            File.AppendAllLines(path, lines);
        }
    }
}
