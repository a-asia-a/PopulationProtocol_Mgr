using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.IO;

namespace PopulationProtocolApp
{
    class Result
    {
        public long numberOfInteraction { get; set; }
        public TimeSpan timeOfSimulation { get; set; }
        public string resultSymbolOfOneSimulation { get; set; }

        public Result()
        {
            resultSymbolOfOneSimulation = "";
            numberOfInteraction = 0;
            timeOfSimulation = new TimeSpan();
        }
    }


    class Simulation
    {

        private bool ifSimulationConverge;
        private long maxNumberOfInteraction;
        public int numberOfRepetitions;
        public long numberOfInteraction { get; private set; }
        public ObservableCollection<Result> listResultSimulation { get; private set; }
        private Random random;
        public static Dictionary<string, int> outputOfAllSimulation { get; private set; }
        public TimeSpan timeOfAllSimulation { get; private set; }
        public static long numberOfAllInteraction { get; set; }

        public Simulation()
        {
            ifSimulationConverge = false;
            maxNumberOfInteraction = 9000000000000000000;
            numberOfInteraction = 0;
            listResultSimulation = new ObservableCollection<Result>();
            random = new Random();
            outputOfAllSimulation = new Dictionary<string, int>();
            timeOfAllSimulation = new TimeSpan();
            numberOfAllInteraction = 0;

        }

        public void startSimulation()
        {
            // read protocol from file
            if (Protocol.Instance.saveProtocolFromFileToVariable())
            {
                int i = 0;
                // create graph
                Graph graph = new Graph();
                while (i < numberOfRepetitions)
                {
                   
                    Stopwatch timer = new Stopwatch();

                    // run single simulstion and measure time simulation
                    timer.Start();
                    runInputFunction();
                    ifSimulationConverge = runStatesFunction();
                    timer.Stop();

                    // save result of one simulation
                    Result result = new Result();

                    if (ifSimulationConverge && Protocol.Instance.ifLiderChooseProtocol == true)
                        result.resultSymbolOfOneSimulation = Graph.Instance.outputSymbolOfLider;
                    else if (ifSimulationConverge && Protocol.Instance.ifLiderChooseProtocol == false)
                        result.resultSymbolOfOneSimulation = getResultOfSimulation();
                    else
                        result.resultSymbolOfOneSimulation = "Symulation doesn't converge";

                    result.numberOfInteraction = this.numberOfInteraction;
                    result.timeOfSimulation = timer.Elapsed;
                    listResultSimulation.Add(result);

                    i++;
                    Graph.Instance.clearGraph();
                    
                    //Console.WriteLine(i + "koniec iteracji ");
                    //graph = null;
                }

                summaryAllSimulation();
                saveResult();
            }
        }

        private void runInputFunction()
        {
            foreach (Node node in Graph.Instance.setNodesInGraph)
            {
                node.stateOfNode = Protocol.Instance.getResultFunction(
                                        Protocol.Instance.inputFunctionOfProtocol,
                                        node.initializeInputOfNode);
            }
        }    

        private void runOutputFunction(Tuple<Node, Node> pairOfNode)
        {
            pairOfNode.Item1.outputOfNode = Protocol.Instance.getResultFunction(
                                                Protocol.Instance.outputFunctionOfProtocol,
                                                pairOfNode.Item1.stateOfNode);

            pairOfNode.Item2.outputOfNode = Protocol.Instance.getResultFunction(
                                                Protocol.Instance.outputFunctionOfProtocol,
                                                pairOfNode.Item2.stateOfNode);
        }
        private void runOutputFunctionForAll()
        {
            foreach (Node node in Graph.Instance.setNodesInGraph)
            {
                node.outputOfNode = Protocol.Instance.getResultFunction(
                                                Protocol.Instance.outputFunctionOfProtocol,
                                                node.stateOfNode);
            }
        }

        public Tuple<Node, Node> randNodesToInteraction()
        {
            //random 2 diffrent nodes
            int rnd1 = random.Next(0, Graph.Instance.setNodesInGraph.Count);
            int rnd2 = random.Next(0, Graph.Instance.setNodesInGraph.Count);
            while (rnd2 == rnd1)
            {
                rnd2 = random.Next(0, Graph.Instance.setNodesInGraph.Count);
            }

            return new Tuple<Node, Node>(Graph.Instance.setNodesInGraph[rnd1],
                                        Graph.Instance.setNodesInGraph[rnd2]);
        }

        private bool runStatesFunction()
        {
            numberOfInteraction = 0;
            Tuple<Node, Node> nodeToInteraction = randNodesToInteraction();
            Tuple<string, string> newStates = new Tuple<string, string>("","");
            runOutputFunctionForAll();

            //to debug
            //showAll();

            bool status = false;
            while (!status)
            {
                if (numberOfInteraction > maxNumberOfInteraction)
                {
                    return false;
                }
                numberOfInteraction++;
                //Console.WriteLine(numberOfInteraction);
                nodeToInteraction = randNodesToInteraction();

                //to debug
                //showNodeToInteract(nodeToInteraction);

                newStates = Graph.Instance.getNewStatesOfNodes(nodeToInteraction);
                nodeToInteraction.Item1.stateOfNode = newStates.Item1;
                nodeToInteraction.Item2.stateOfNode = newStates.Item2;

                //to debug
                //showAll();

                runOutputFunction(nodeToInteraction);

                if (Protocol.Instance.ifLiderChooseProtocol)
                    status = Graph.Instance.oneLiderInGraph();
                else
                    status = Graph.Instance.allNodesHaveTheSameOutput();

            }
            //Console.WriteLine("------------------------------------------------------------");
            return true;
        }

        private string getResultOfSimulation()
        {
            return Graph.Instance.setNodesInGraph[0].outputOfNode;
        }

        private void showAll()
        {
            foreach (Node node in Graph.Instance.setNodesInGraph)
            {
                Console.Write(node.stateOfNode + " ");
            }
            Console.Write(Environment.NewLine);
           
        }

        private void showNodeToInteract(Tuple<Node, Node> nodeToInteraction)
        {
            Console.Write("   :   " + nodeToInteraction.Item1.stateOfNode + "," +
                                nodeToInteraction.Item2.stateOfNode);
            Console.Write(Environment.NewLine);
        }

        private void saveResult()
        {
            
            using (StreamWriter newFile = new StreamWriter(MainWindow.pathToResultFile))
            {
                newFile.WriteLine("# *****************************************************************************************************");
                newFile.WriteLine("#                                          RESULT");
                newFile.WriteLine("# *****************************************************************************************************");
                newFile.WriteLine("# Path to protocol:" + MainWindow.pathToProtocolFile);
                newFile.WriteLine("# Path to graph:" + MainWindow.pathToGraphFile);
                newFile.WriteLine("# *****************************************************************************************************");
                newFile.WriteLine("# INFO ABOUT GRAPH:");
                newFile.WriteLine("# number of nodes: " + MainWindow.numberOfNodes);

                foreach (KeyValuePair<string, int> kvp in MainWindow.inputAlphabetTemp)
                {
                    string txt = "# number of nodes with symbol '" + kvp.Key + "':" + Convert.ToString(kvp.Value);
                    newFile.WriteLine(txt);
                }
                newFile.WriteLine("# *****************************************************************************************************");
                newFile.WriteLine("# INFO ABOUT PROTOCOL:");
                newFile.Write('\n' + "# input alphabet: ");
                foreach (var x in Protocol.Instance.inputAlphabetOfProtocol)
                {
                    newFile.Write(x + "; ");
                }
                newFile.Write(System.Environment.NewLine);
                newFile.Write("# states: ");
                foreach (var x in Protocol.Instance.statesOfProtocol)
                {
                    newFile.Write(x + "; ");
                }
                newFile.Write(System.Environment.NewLine);
                newFile.Write("# output alphabet: ");
                foreach (var x in Protocol.Instance.outputAlphabetOfProtocol)
                {
                    newFile.Write(x + "; ");
                }
                newFile.Write(System.Environment.NewLine);
                newFile.Write('\n' + "# input function: ");
                newFile.Write(System.Environment.NewLine);
                foreach (var x in Protocol.Instance.inputFunctionOfProtocol)
                {
                    newFile.WriteLine("#   " + x.Key + " -> " + x.Value);
                }
                newFile.WriteLine("# states function: ");
                foreach (var x in Protocol.Instance.statesFunctionOfProtocol)
                {
                    newFile.WriteLine("#   " + x.Key.Item1 + "," + x.Key.Item2
                        + " -> " + x.Value.Item1 + "," + x.Value.Item2);
                }
                newFile.WriteLine("# output function: ");
                foreach (var x in Protocol.Instance.outputFunctionOfProtocol)
                {
                    newFile.WriteLine("#   " + x.Key + " -> " + x.Value);
                }
                newFile.WriteLine("# *****************************************************************************************************");
                newFile.WriteLine("# RESULT SIMULATION:");
                newFile.WriteLine("# -----------------------------------------------------------------------------------------------------");

                int i = 1;
                foreach (var result in listResultSimulation)
                {
                    newFile.WriteLine("# simulation " + i + "/" + numberOfRepetitions);
                    newFile.WriteLine("output: '" + result.resultSymbolOfOneSimulation + "'" + '\n');
                    newFile.WriteLine("time: " + result.timeOfSimulation.TotalMilliseconds + " ms");
                    newFile.WriteLine("number of interaction: " + result.numberOfInteraction);
                    newFile.WriteLine("# -----------------------------------------------------------------------------------------------------");
                    i++;
                }
                newFile.WriteLine("# *****************************************************************************************************");
                newFile.WriteLine("# STATISTIC OF ALL SIMULATIONS:");
                newFile.WriteLine("# -----------------------------------------------------------------------------------------------------");
                newFile.WriteLine("number of simulation: " + numberOfRepetitions);

                foreach (var symbol in outputOfAllSimulation)
                {
                    newFile.WriteLine("output: '" + symbol.Key + "': " + symbol.Value + "/" + numberOfRepetitions +
                        " => " + Convert.ToDouble(symbol.Value * 100 / numberOfRepetitions) + " %");
                }
                newFile.WriteLine("average time od one simulation: " +
                    timeOfAllSimulation.TotalMilliseconds / numberOfRepetitions + " ms");
                newFile.WriteLine("number of interaction: " +
                    (int)numberOfAllInteraction / numberOfRepetitions);
                newFile.WriteLine("# -----------------------------------------------------------------------------------------------------");
            }
        }

        private void summaryAllSimulation ()
        {
            foreach (var result in listResultSimulation)
            {
                if (!outputOfAllSimulation.ContainsKey(result.resultSymbolOfOneSimulation))
                    outputOfAllSimulation.Add(result.resultSymbolOfOneSimulation, 1);
                else
                    outputOfAllSimulation[result.resultSymbolOfOneSimulation] += 1;

                timeOfAllSimulation += result.timeOfSimulation;
                numberOfAllInteraction += result.numberOfInteraction;
            }
        }
            

    }
}


