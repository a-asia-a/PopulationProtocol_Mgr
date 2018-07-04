using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationProtocolApp
{
    class Graph
    {
        private static Graph instance;

        public List<Node> setNodesInGraph { get; private set; } 
        public int numberOfNodesInGraph { get; private set; }
        //public bool resultTempOfProtocol { get; private set; }
        //public bool resultOfProtocol { get; private set; } // ??????
        public string outputSymbolOfLider = "";

        public Graph()
        {
            
            setNodesInGraph = createGraph();
            //random = new Random();
        }
        ~Graph()
        {
            instance = null;
        }
        public static Graph Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Graph();
                }
                return instance;
            }
        }

        private List<Node> createGraph()
        {
            string pathFile = MainWindow.pathToGraphFile;
            List<Node> set = new List<Node>();

            using (StreamReader file = new StreamReader(pathFile))
            {
                try
                {
                    string line = file.ReadLine();
                    numberOfNodesInGraph = Convert.ToInt32(file.ReadLine());

                    while (!file.EndOfStream)
                    {
                        set.Add(new Node(file.ReadLine()));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("CreateGraph - The file could not be read:");
                    Console.WriteLine(e.Message);
                }
                return set;
            }
        }
        
        private void clearSet()
        {
            setNodesInGraph.Clear();
        }

        /*
        public Tuple<string, string> oneIteractionOfStatesFunction()
        {
            //random 2 diffrent nodes
            int rnd1 = random.Next(0, setNodesInGraph.Count);
            int rnd2 = random.Next(0, setNodesInGraph.Count);
            while (rnd2 == rnd1)
            {
                rnd2 = random.Next(0, setNodesInGraph.Count);
            }

            Tuple<string, string> newStatesOfNodes = new Tuple<string, string>("","");
            string stateOfInitiatorNode = setNodesInGraph[rnd1].stateOfNode;
            string stateOfResponderNode = setNodesInGraph[rnd2].stateOfNode;

            newStatesOfNodes = Protocol.Instance.getResultFunctionTransition(
                                Protocol.Instance.statesFunctionOfProtocol, 
                                stateOfInitiatorNode, stateOfResponderNode);

            // todo - przeniesc do funkcji z interakcjami
            //setNodesInGraph[rnd1].setStateOfNode(newStatesOfNodes.first);
            //setNodesInGraph[rnd2].setStateOfNode(newStatesOfNodes.second);

            return newStatesOfNodes;
        }

        */
        
        public Tuple<string, string> getNewStatesOfNodes(Tuple<Node, Node> pairOfNodes)
        {
            Tuple<string, string> newStatesOfNodes = new Tuple<string, string>("", "");
            string stateOfInitiatorNode = pairOfNodes.Item1.stateOfNode;
            string stateOfResponderNode = pairOfNodes.Item2.stateOfNode;

            newStatesOfNodes = Protocol.Instance.getResultFunctionTransition(
                                Protocol.Instance.statesFunctionOfProtocol,
                                stateOfInitiatorNode, stateOfResponderNode);

            return newStatesOfNodes;
        }

        public bool allNodesHaveTheSameOutput()
        {
            bool status = false;
            string outputNodeRef = setNodesInGraph[0].outputOfNode;
           // if (outputNodeRef != "b" && outputNodeRef != "B")
            //{
                foreach (Node node in setNodesInGraph)
                {
                    if (node.outputOfNode != outputNodeRef)
                        return false;
                }
                return true;
           // }
            //else
               // return false;
        }

        public bool oneLiderInGraph()
        {
            //bool status = false;
            int sum = 0;
            foreach (Node node in setNodesInGraph)
            {
                sum += Convert.ToInt32(node.outputOfNode);
                if (node.outputOfNode == "1")
                    outputSymbolOfLider = node.stateOfNode;
            }
            if (sum == 1)
                return true;
            else
                return false;
        }

        public void clearGraph()
        {
            foreach (Node node in setNodesInGraph)
            {
                node.outputOfNode = null;
                node.stateOfNode = null;
            }
        }
    }
}
