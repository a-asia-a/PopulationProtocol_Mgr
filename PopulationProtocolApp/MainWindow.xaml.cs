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
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace PopulationProtocolApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowGraph windowGraph = new WindowGraph();
        }

        public static Dictionary<string, int> inputAlphabetTemp = new Dictionary<string, int>();
        public static int numberOfNodes { get; set; }
        public static string protocolFileName { get; private set; }
        public static string pathToGraphFile { get; private set; }
        public static string pathToProtocolFile { get; set; }
        public static string pathToResultFile { get; private set; }
        private FolderBrowserDialog folderBrowserDialog;
        public int numberOfRepetitions { get; set; }
        public static bool statusCreateGraph { get; set; }
        public static bool statusCreateProtocol { get; set; }
        public static string configurationGaph {get; private set;}

        // nieużywane - stare - jak sie tego pozbyc?
        //private void button_selectProtocol_Click(object sender, RoutedEventArgs e) {}    
        private void button_protocolCreate_Click_1(object sender, RoutedEventArgs e) {}

        private void btnProtocolCreate_Click_1(object sender, RoutedEventArgs e)
        {
            ProtocolWindow windowProtocol = new ProtocolWindow();
            windowProtocol.RefToMainForm = this;
            this.Hide();
            windowProtocol.Show();
            statusCreateGraph = false;
            statusCreateProtocol = false;
            btnLoadNewProtocol.IsEnabled = true;
            btnLoadNewGraph.IsEnabled = true;
        }

        private void btnProtocolFromFile_Click(object sender, RoutedEventArgs e)
        {
            bool status = false;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // czy jest jakas zmienna srodowiskowa mowiaca o folderze w którym znajduje sie projekt??

            openFileDialog.InitialDirectory = @"c:\repozytoria\PopulationProtocolMgr\protocols";
            if (openFileDialog.ShowDialog() == true)
            {
                pathToProtocolFile = openFileDialog.FileName;

                //load info about protocol to richBox and activate field's to add a graph 
                status = validateFile(pathToProtocolFile, "protocol");
                if (status)
                    status = createFileWithProtocolInfo(pathToProtocolFile);
                else
                {
                    pathToProtocolFile = "";
                    txtPathProtocol.Text = pathToProtocolFile;
                    txtInfoProtocol.Document.Blocks.Clear();
                    txtInfoProtocol.AppendText("No protocol uploaded.");
                    //System.Windows.MessageBox.Show("This file doesn't contains protocol! Select other file.");
                    groupBxGraphSettings.IsEnabled = false;
                }

                if (status)
                {
                    clearGraphAndSimulationField();
                    txtPathProtocol.Text = pathToProtocolFile;
                    loadInfoProtocolFromFile();
                    inputAlphabetTemp = createDictionaryWithInputAlphabet(pathToProtocolFile);
                    groupBxGraphSettings.IsEnabled = true;
                    btnLoadNewGraph.IsEnabled = false;
                }
            }      
        }

        private void btnGraphFromFile_Click(object sender, RoutedEventArgs e)
        {
            bool status = false;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // czy jest jakas zmienna srodowiskowa mowiaca o folderze w którym znajduje sie projekt??
            openFileDialog.InitialDirectory = @"c:\repozytoria\PopulationProtocolMgr\graphs";
            if (openFileDialog.ShowDialog() == true)
            {
                txtPathGraph.Text = openFileDialog.FileName;

                status = validateFile(txtPathGraph.Text, "graph");
                if (status)
                {
                    status = prepareGraphInfo(txtPathGraph.Text);
                }
                else
                {
                    System.Windows.MessageBox.Show("This file doesn't contains file with graph!");
                    txtPathGraph.Text = "";
                    txtInfoGraph.Document.Blocks.Clear();
                    txtInfoGraph.AppendText("No graph uploaded.");
                }

                if (status)
                {
                    pathToGraphFile = txtPathGraph.Text;
                    loadGraphInfo();
                    loadSimulationGrpbx();
                }
            }
        }

        private bool validateGraph()
        {
            string pathFile = txtPathGraph.Text;
            using (StreamReader file = new StreamReader(pathFile))
            {
                try
                {
                    string line = file.ReadLine();
                    int numberOfNodesTemp = Convert.ToInt32(file.ReadLine());
                    int numberSymbolInFile = 0;
                    
                    while (!file.EndOfStream)
                    {
                        if (!inputAlphabetTemp.ContainsKey(file.ReadLine()))
                        {
                            System.Windows.MessageBox.Show("This graph doesn't match to Protocol. Select other file.");
                            txtPathGraph.Text = "";
                            txtInfoGraph.Document.Blocks.Clear();
                            txtInfoGraph.AppendText("No graph uploaded.");
                            grpbxSimulation.IsEnabled = false;
                            return false;
                        }
                        numberSymbolInFile++;
                    }
                    if (numberOfNodesTemp == numberSymbolInFile)
                    {
                        //numberSymbolInFile = 0;
                        return true;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Incorrect number of nodes in graph. Correct number of nodes in file.");
                        txtPathGraph.Text = "";
                        txtInfoGraph.Document.Blocks.Clear();
                        txtInfoGraph.AppendText("No graph uploaded.");
                        grpbxSimulation.IsEnabled = false;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Validate graph - The file could not be read:");
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        private bool validateFile(string pathFile, string goal)
        {
            string line;

            if (pathFile == "")
            {
                System.Windows.MessageBox.Show("No selected file.");
                return false;
            }
            else
            {
                StreamReader file = new System.IO.StreamReader(pathFile);
                line = file.ReadLine();

                if (goal == "protocol" && line == "<input>")
                    return true;
                else if (goal == "graph" && line == "#graph")
                {
                    if (validateGraph())
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (goal == "protocol")
                        System.Windows.MessageBox.Show("This file doesn't contains protocol! Select other file.");
                    return false;
                }
            }       
        }

        private bool createFileWithProtocolInfo(string pathFile)
        {
            // Create a temporary file, and put some data into it.
            //string path = System.IO.Path.GetTempFileName();

            const Int32 BufferSize = 128;
            File.Create("infoProtocol.txt").Dispose();
            using (StreamWriter newFile = new StreamWriter("infoProtocol.txt"))
            { 
                using (StreamReader file = new StreamReader(pathFile, Encoding.UTF8, true, BufferSize))
                {
                    try
                    {
                        string line = file.ReadLine();
                        while (!file.EndOfStream)
                        {
                            if (!line.Contains("</"))
                            {
                                if ("<input>" == line)
                                    newFile.WriteLine("input alphabet: ");
                                else if ("<output>" == line)
                                    newFile.WriteLine("output alphabet: ");
                                else if ("<states>" == line)
                                    newFile.WriteLine("states: ");
                                else if ("<inputFunction>" == line)
                                    newFile.WriteLine("input function: ");
                                else if ("<outputFunction>" == line)
                                    newFile.WriteLine("output function: ");
                                else if ("<statesFunction>" == line)
                                    newFile.WriteLine("states function: ");
                                else
                                    newFile.WriteLine(" " + line);
                            }
                            line = file.ReadLine();
                        }
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Create file with info protocol - The file could not be read:");
                        Console.WriteLine(e.Message);
                        return false;
                    }
                }
            }         
        } 

        private void loadInfoProtocolFromFile()
        {
            txtInfoProtocol.Document.Blocks.Clear();
            using (StreamReader file = new StreamReader("infoProtocol.txt"))
            {
                try
                {
                    while (!file.EndOfStream)
                    {
                        txtInfoProtocol.AppendText(file.ReadLine());
                        txtInfoProtocol.AppendText(Environment.NewLine);
                    }
                    //File.Delete("infoProtocol.txt");
                }
                catch (Exception e)
                {
                    Console.WriteLine("load info protocol - The infoProtocol could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
        }

        private Dictionary<string,int> createDictionaryWithInputAlphabet(string pathFile)
        {
            
            using (StreamReader file = new StreamReader(pathFile))
            {
                Dictionary<string, int> dictionaryTemp = new Dictionary<string, int>();
                try
                {
                    string line = file.ReadLine();
                    if (line == "<input>")
                    {
                        line = file.ReadLine();
                        while (!line.Contains("</"))
                        {
                            dictionaryTemp.Add(line, 0);
                            line = file.ReadLine();
                        }
                    }
                    return dictionaryTemp;
                }
                catch (Exception e)
                {
                    Console.WriteLine("create dictionary with input alphabet - The file could not be read:");
                    Console.WriteLine(e.Message);
                }
                return dictionaryTemp;
            } 
        }

        private bool prepareGraphInfo(string pathFile)
        {
            clearValueInDictionary();
            const Int32 BufferSize = 128;
            using (StreamReader file = new StreamReader(pathFile, Encoding.UTF8, true, BufferSize))
            {
                try
                {
                    file.ReadLine();
                    numberOfNodes = Convert.ToInt32(file.ReadLine());

                    while (!file.EndOfStream)
                    {
                        string symbol = file.ReadLine();
                        if (inputAlphabetTemp.ContainsKey(symbol))
                            inputAlphabetTemp[symbol] += 1;
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("prepare graph info - The file could not be read:");
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        private void clearValueInDictionary()
        {
            List<string> keys = new List<string>(inputAlphabetTemp.Keys);
            foreach (var key in keys)
            {
                inputAlphabetTemp[key] = 0;
            }
        }

        private void loadGraphInfo()
        {
            txtInfoGraph.Document.Blocks.Clear();
            txtInfoGraph.AppendText("number of nodes: " + numberOfNodes);
            txtInfoGraph.AppendText(Environment.NewLine);

            foreach (KeyValuePair<string, int> kvp in inputAlphabetTemp)
            {
                string txt = "number of nodes with symbol - " + Convert.ToString(kvp.Key) +
                             ": " + Convert.ToString(kvp.Value) + Environment.NewLine;
                txtInfoGraph.AppendText(txt);
            }
        }

        private string renameGraphFile()
        {
            string newFileName = "graph_" + numberOfNodes + "nodes_";
            foreach (KeyValuePair<string, int> kvp in inputAlphabetTemp)
            {
                if (kvp.Value != 0)
                {
                    newFileName += Convert.ToString(kvp.Value) + Convert.ToString(kvp.Key) + "_";
                }
            }
            newFileName += DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".txt";

            string newPath = @"C:\repozytoria\PopulationProtocolMgr\graphs\" + newFileName;
            return newPath;
        }

        private void loadSimulationGrpbx()
        {
            grpbxSimulation.IsEnabled = true;
            txtxbxFileNameResult.Text = createResultFileName();
            txtbxPathResult.Text = @"C:\repozytoria\PopulationProtocolMgr\results\";

            if (txtxbxFileNameResult.Text != "" && txtbxPathResult.Text != "")
            {
                pathToResultFile = txtbxPathResult.Text + txtxbxFileNameResult.Text;
                btnStart.IsEnabled = true;
            }      
            else
                btnStart.IsEnabled = false;
        }

        private string createResultFileName()
        {
            string graphFilenameTmp = System.IO.Path.GetFileNameWithoutExtension(pathToGraphFile);
            int idx = graphFilenameTmp.LastIndexOf('_');
            graphFilenameTmp = graphFilenameTmp.Remove(idx, graphFilenameTmp.Length - idx);

            return string.Concat(
                "result_",
                System.IO.Path.GetFileNameWithoutExtension(pathToProtocolFile),
                "_",
                graphFilenameTmp,
                "_",
                DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss"),
                ".txt"
                );
        }
        
        private void btnGraphCreate_Click(object sender, RoutedEventArgs e)
        {
            protocolFileName = System.IO.Path.GetFileName("@" + txtPathProtocol.Text);

            WindowGraph windowGraph = new WindowGraph();
            windowGraph.RefToMainForm = this;
            this.Hide();
            windowGraph.Show();
            btnLoadNewGraph.IsEnabled = true;

        }

        private void btnLoadNewGraph_Click(object sender, RoutedEventArgs e)
        {
            if (WindowGraph.pathToGraphFile == null)
                System.Windows.MessageBox.Show("No graph to load!");
            else
            {
                prepareGraphInfo(WindowGraph.pathToGraphFile);

                string newPathToGraphFile = renameGraphFile();
                System.IO.File.Move(WindowGraph.pathToGraphFile, newPathToGraphFile);
                pathToGraphFile = newPathToGraphFile;
                txtPathGraph.Text = pathToGraphFile;

                loadGraphInfo();
                loadSimulationGrpbx();
            }      
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {            
            using (folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = @"C:\repozytoria\PopulationProtocolMgr\results\";
                System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
                
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string files = folderBrowserDialog.SelectedPath;
                    txtbxPathResult.Text = files;       
                }
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            bool status = true;
            if (txtxbxFileNameResult.Text == "")
            {
                status = false;
                System.Windows.MessageBox.Show("No file name for simulation result!");
            }
            if (txtbxPathResult.Text == "")
            {
                status = false;
                System.Windows.MessageBox.Show("No director for the file with simulation result!");
            }
            if (txtNumberRepetitions.Text == "")
            {
                status = false;
                System.Windows.MessageBox.Show("The number of simulation repeats must be at least 1!");
            }

            if (status)
            { 
                // run simulation
                Simulation simulation = new Simulation();
                simulation.numberOfRepetitions = Convert.ToInt32(txtNumberRepetitions.Text);
                simulation.startSimulation();


                //TODO create form with result - dopracuj co jesli sa rowne
                string inputMajority = "";
                int tmp = 0;
                foreach (KeyValuePair<string, int> kvp in inputAlphabetTemp)
                {
                    if (kvp.Value > tmp && (kvp.Key != "b" || kvp.Key != "B"))
                    {
                        inputMajority = kvp.Key;
                        tmp = kvp.Value;
                    }
                }
               
                string txtConfiguration = "";
                foreach (KeyValuePair<string, int> kvp in inputAlphabetTemp)
                {
                    if (kvp.Value != 0)
                        txtConfiguration += Convert.ToString(kvp.Value) + Convert.ToString(kvp.Key) + " ";      
                }

                int averageTime = (int)Simulation.numberOfAllInteraction / numberOfRepetitions;
                string stateMajor = Protocol.Instance.getResultFunction(Protocol.Instance.inputFunctionOfProtocol, inputMajority);
                string correctOutput = Protocol.Instance.getResultFunction(Protocol.Instance.outputFunctionOfProtocol, stateMajor);

                string result = "";
                double correctness = 0;
                foreach (var symbol in Simulation.outputOfAllSimulation)
                {
                    double tempCorrectnes = Convert.ToDouble(symbol.Value) * 100 / Convert.ToDouble(numberOfRepetitions);
                    result += "output '" + symbol.Key + "': " + symbol.Value + " / " + numberOfRepetitions + " => " + tempCorrectnes + "%" + Environment.NewLine;
                    if (symbol.Key == correctOutput)
                        correctness = tempCorrectnes;
                }
               

                System.Windows.MessageBox.Show("End of simulation!" + Environment.NewLine + Environment.NewLine +
                    "protocol filename: " + System.IO.Path.GetFileName("@" + txtPathProtocol.Text) + Environment.NewLine +
                    "number of nodes: " + numberOfNodes + Environment.NewLine +
                    "start configuration: " + txtConfiguration + Environment.NewLine +                  
                    "number of simulation: " + txtNumberRepetitions.Text + Environment.NewLine +
                    "  " + Environment.NewLine +
                    "correct output: '" + correctOutput +"'" + Environment.NewLine +
                    result +
                    "average number of interaction: " + averageTime + Environment.NewLine +
                    Environment.NewLine +
                    "----------------------------------------------------" +Environment.NewLine +
                    "protocol correctness [%]: " + correctness + Environment.NewLine +
                    "parallel time: " + (double)averageTime/numberOfNodes);
                
                clearProtocol();
            }
        }
       
        private void txtxbxFileNameResult_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtxbxFileNameResult.Text == "")
                btnStart.IsEnabled = false;
            else
            {
                pathToResultFile = txtbxPathResult.Text + txtxbxFileNameResult.Text;
                btnStart.IsEnabled = true;
            }             
        }

        private void txtbxPathResult_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtxbxFileNameResult.Text == "")
                btnStart.IsEnabled = false;
            else
                btnStart.IsEnabled = true;
            //pathToResultFile = txtbxPathResult.Text + txtxbxFileNameResult.Text;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            pathToGraphFile = "";
            pathToResultFile = "";
            clearProtocol();
            clearGraphAndSimulationField();
        }

        private void txtNumberRepetitions_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtNumberRepetitions.Text != "")
                numberOfRepetitions = Convert.ToInt32(txtNumberRepetitions.Text);
        }

        private void txtNumberRepetitions_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 || key == 2 || key == 8);
        }

        private void clearGraphAndSimulationField()
        {
            txtbxPathResult.Text = "";
            txtxbxFileNameResult.Text = "";
            grpbxSimulation.IsEnabled = false;

            txtPathGraph.Text = "";
            txtInfoGraph.Document.Blocks.Clear();
            txtInfoGraph.AppendText("No graph uploaded.");
            btnLoadNewGraph.IsEnabled = false;
        }

        private void clearProtocol()
        {
            Protocol.Instance.ifLiderChooseProtocol = false;
            Protocol.Instance.inputAlphabetOfProtocol.Clear();
            Protocol.Instance.statesOfProtocol.Clear();
            Protocol.Instance.outputAlphabetOfProtocol.Clear();
            Protocol.Instance.inputFunctionOfProtocol.Clear();
            Protocol.Instance.outputFunctionOfProtocol.Clear();
            Protocol.Instance.statesFunctionOfProtocol.Clear();
            Protocol.Instance.resultOfOutputProtocol.Clear();
        }

        private void txtPathGraph_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPathGraph.Text == "")
                btnStart.IsEnabled = false;
            else
            {
                pathToResultFile = txtbxPathResult.Text + txtxbxFileNameResult.Text;
                btnStart.IsEnabled = true;
            }
        }

        private void btnLoadNewProtocol_Click(object sender, RoutedEventArgs e)
        {
            bool status = false;
            
            //load info about protocol to richBox and activate field's to add a graph 
            status = validateFile(pathToProtocolFile, "protocol");
            if (status)
                status = createFileWithProtocolInfo(pathToProtocolFile);
            else
            {
                pathToProtocolFile = "";
                txtPathProtocol.Text = pathToProtocolFile;
                txtInfoProtocol.Document.Blocks.Clear();
                txtInfoProtocol.AppendText("No protocol uploaded.");
                groupBxGraphSettings.IsEnabled = false;
            }

            if (status)
            {
                clearGraphAndSimulationField();
                txtPathProtocol.Text = pathToProtocolFile;
                loadInfoProtocolFromFile();
                inputAlphabetTemp = createDictionaryWithInputAlphabet(pathToProtocolFile);
                groupBxGraphSettings.IsEnabled = true;
                btnLoadNewGraph.IsEnabled = false;
            }
        }
    }
}
