using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StatusBarHelper statusBarHelper;
        RectCreation rectCreation;
        ConnectionCreation connectionCreation;
        RectLocation rectLocation;
        DataAddition dataAddition;
        SpanningTree spanningTree;
        Deletion deletion;
        DFS DFS;
        BFS BFS;
        Dijkstra dijkstra;
        MaxFlow maxFlow;
        public MainWindow()
        {
            InitializeComponent();
            Tool.Vertices = FileHelper.ReadGraph("C:\\Users\\Пользователь\\Downloads\\lab5\\lab5\\testRead.txt");
            if (Tool.Vertices == null)
            {
                Tool.Vertices = new List<Vertex>();
            }
            GraphDrawing treeDrawing = new GraphDrawing(CnvBack);

            statusBarHelper = new StatusBarHelper(LblCoordinates);
            rectLocation = new RectLocation(CnvBack, statusBarHelper);
        }
        private void BtnRect_Click(object sender, RoutedEventArgs e)
        {
            rectCreation = new RectCreation(CnvBack);
        }

        private void BtnConect_Click(object sender, RoutedEventArgs e)
        {
            connectionCreation = new ConnectionCreation(CnvBack);
        }

        private void BtnData_Click(object sender, RoutedEventArgs e)
        {
            dataAddition = new DataAddition(CnvBack);
        }

        private void BtnSpannigTree_Click(object sender, RoutedEventArgs e)
        {
            spanningTree = new SpanningTree(CnvBack);
        }

        private void BtnSaveTree_Click(object sender, RoutedEventArgs e)
        {
            FileHelper.SaveGraph(Tool.Vertices);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            deletion = new Deletion(CnvBack);
        }

        private void BtnDFS_Click(object sender, RoutedEventArgs e)
        {
            DFS = new DFS(CnvBack);
        }

        private void BtnBFS_Click(object sender, RoutedEventArgs e)
        {
            BFS = new BFS(CnvBack);
        }

        private void BtnDijkstra_Click(object sender, RoutedEventArgs e)
        {
            dijkstra = new Dijkstra(CnvBack);
        }

        private void BtnMaxFlow_Click(object sender, RoutedEventArgs e)
        {
            //  maxFlow = new MaxFlow(CnvBack);
            FlowAlgorithm flow = new FlowAlgorithm(CnvBack);
            flow.run();
        }
    }
}
