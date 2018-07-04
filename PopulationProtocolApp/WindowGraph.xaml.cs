using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PopulationProtocolApp
{
    /// <summary>
    /// Interaction logic for WindowGraph.xaml
    /// </summary>
    public partial class WindowGraph : Window
    {

        private int numberOfNodesTemp = 0;
        public static string pathToGraphFile { get; private set; }
        private List<string> listOfNodesInGraph;
        public Window RefToMainForm { get; set; }
        private Random random;
       // public static Dictionary<string, int> numberOfSymbolInGraph { get; private set; }
        public ObservableCollection<InputSymbol> numberOfSymbolInGraph = new ObservableCollection<InputSymbol>();

        public WindowGraph()
        {
            InitializeComponent();
            addInputAlpabetToList();
            listNode.ItemsSource = numberOfSymbolInGraph;
            random = new Random();
           // numberOfSymbolInGraph = new Dictionary<string, int>();
        }

        private void addInputAlpabetToList()
        {
            foreach (KeyValuePair<string, int> kvp in MainWindow.inputAlphabetTemp)
            {
                cmbBxInputAlphabet.Items.Add(kvp.Key);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e){}
       

        private void txtBxNumberOfNodes_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBxNumberOfNodes.Text != "")
                numberOfNodesTemp = Convert.ToInt32(txtBxNumberOfNodes.Text);
        }

        private void txtBxNumberOfNodes_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 || key == 2 || key == 8);
        }

        private void txtBxNumberOfSymbol_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 || key == 2 || key == 8);
        }

        private void checkBxIfRandom_Checked(object sender, RoutedEventArgs e)
        {
            groupBxDefineGraph.IsEnabled = false;
            txtBxNumberOfNodes.IsEnabled = true;
            labNumberOfNodes.IsEnabled = true;
        }

        private void checkBxIfRandom_Unchecked(object sender, RoutedEventArgs e)
        {
            groupBxDefineGraph.IsEnabled = true;
            txtBxNumberOfNodes.IsEnabled = false;
            labNumberOfNodes.IsEnabled = false;
        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            bool status = false;
            if (checkBxIfRandom.IsChecked == true)
            {
                if (txtBxNumberOfNodes.Text != "")
                {
                    listOfNodesInGraph = randomGraph();
                    status = true;
                }
                else
                    MessageBox.Show("Enter the number of nodes.");
            }
            else
            {
                //status = validDefineGraph();
                numberOfNodesTemp = sumOfNodesInGraph();
                listOfNodesInGraph = transformGraphFieldToList();
                status = true;
            }
            if (status)
            {
                pathToGraphFile = createFileWithGraph(listOfNodesInGraph);
                MainWindow.statusCreateGraph = true;
                //MainWindow.delegateToLoadBtn();
                this.Close();
                this.RefToMainForm.Show();
            } 
        }

        private string randomInputForNode(List<string> list)
        {
            int rnd = random.Next(0, MainWindow.inputAlphabetTemp.Count);
            return list[rnd];
        }
       
        private List<string> randomGraph()
        {
            List<string> listAvailableSymbol = new List<string>();
            foreach (KeyValuePair<string, int> kvp in MainWindow.inputAlphabetTemp)
            {
                listAvailableSymbol.Add(kvp.Key);
            }

            List<string> listRandomSymbols = new List<string>();
            for (int i = 0; i < numberOfNodesTemp; i++)
            {
                listRandomSymbols.Add(randomInputForNode(listAvailableSymbol));
            }

            return listRandomSymbols;
        }

        private string createFileWithGraph(List<string> listOfSymbol)
        {
            //create filename
            //string fileNameGraph = createPathGraphFile();
            string pathGraph = @"C:\repozytoria\PopulationProtocolMgr\graphs\graphTemp.txt";
            //File.Create(fileNameGraph).Dispose();
            using (StreamWriter newFile = new StreamWriter(pathGraph))
            {
                try
                {
                    newFile.WriteLine("#graph");
                    newFile.WriteLine(numberOfNodesTemp);
                    for (int i=0; i<numberOfNodesTemp; i++)
                    {
                        newFile.WriteLine(listOfNodesInGraph[i]);
                    }
                    return pathGraph; // System.IO.Path.GetFullPath(fileNameGraph);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error.");
                    Console.WriteLine(e.Message);
                    return "";
                }
            }
        }

        private Dictionary <string, int> createGraphInfo ()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            return dictionary;
        }

        private List<string> transformGraphFieldToList()
        {
            
            List<string> listInputNodesInGraph = new List<string>();
            Random rnd = new Random();

            int i = 0;
            while (listInputNodesInGraph.Count != numberOfNodesTemp)
            {
                int rndValue = rnd.Next(0, numberOfSymbolInGraph.Count);

                if (numberOfSymbolInGraph[rndValue].Number > 0)
                {
                    listInputNodesInGraph.Add(numberOfSymbolInGraph[rndValue].Id);
                    numberOfSymbolInGraph[rndValue].Number -= 1;
                }
                i++;
            }
            return listInputNodesInGraph;
        }

        private int sumOfNodesInGraph()
        {
            int sum = 0;
            foreach (InputSymbol singleNode in numberOfSymbolInGraph)
            {
                sum += singleNode.Number;
            }
            return sum;
        }
       
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            numberOfSymbolInGraph.Add(new InputSymbol { Id = (string)cmbBxInputAlphabet.SelectedItem, Number = Convert.ToInt32(txtBxNumberOfSymbol.Text) });
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listNode.SelectedIndex != -1)
                numberOfSymbolInGraph.RemoveAt(listNode.SelectedIndex);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.statusCreateGraph = false;
            // MainWindow.delegateToLoadBtn();
            this.Close();
            this.RefToMainForm.Show();
           
        }
    }

    public class InputSymbol
    {
        public string Id { get; set; }
        public int Number { get; set; }
    }

}
