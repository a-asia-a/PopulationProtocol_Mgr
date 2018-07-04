using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationProtocolApp
{
    class Node
    {
        public string initializeInputOfNode { get; set; }
        public string stateOfNode { get; set; }
        public string outputOfNode { get; set; }
        public int id { get; set; }

        public Node(string inputSymbol)
        {
            initializeInputOfNode = inputSymbol;
            stateOfNode = null;
            outputOfNode = null;
        }
    }
}
