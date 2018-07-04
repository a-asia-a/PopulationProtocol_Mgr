using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationProtocolApp
{
    class Protocol
    {
        private static Protocol instance = null;

        public HashSet<string> inputAlphabetOfProtocol { get; private set; }
        public HashSet<string> outputAlphabetOfProtocol { get; private set; }
        public HashSet<string> statesOfProtocol { get; private set; }
        public HashSet<int> resultOfOutputProtocol { get; private set; }
        public Dictionary<string, string> inputFunctionOfProtocol { get; private set; }
        public Dictionary<Tuple<string, string>, Tuple<string, string>> statesFunctionOfProtocol { get; private set; }
        public Dictionary<string, string> outputFunctionOfProtocol { get; private set; }
        //public int numberOfNodes { get; private set; }
        public bool ifLiderChooseProtocol { get; set; }

        private Protocol()
        {
            inputAlphabetOfProtocol = new HashSet<string>();
            outputAlphabetOfProtocol = new HashSet<string>();
            statesOfProtocol = new HashSet<string>();
            resultOfOutputProtocol = new HashSet<int>();
            inputFunctionOfProtocol = new Dictionary<string, string>();
            statesFunctionOfProtocol = new Dictionary<Tuple<string, string>, Tuple<string, string>>();
            outputFunctionOfProtocol = new Dictionary<string, string>();
            ifLiderChooseProtocol = false;
        }
        public static Protocol Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Protocol();
                }
                return instance;
            }
        }
        public void addElementToSet(HashSet<string> set, string valueOfElem)
        {
            set.Add(valueOfElem);
        }
        public void deleteElementFromSet(HashSet<string> set, string valueOfElem)
        {
            if (set.Contains(valueOfElem))
                set.Remove(valueOfElem);
            else
                Console.WriteLine("Set " + set + " isn't contain symbol " + valueOfElem);
        }
        public void addFunctionToProtocol(Dictionary<string, string> setFunction, string valueOfElem1, string valueOfElem2)
        {
            setFunction.Add(valueOfElem1, valueOfElem2);
        }
        public void deleteFunctionFromProtocol(Dictionary<string, string> setFunction, string valueOfElem1, string valueOfElem2)
        {
            if (setFunction.ContainsKey(valueOfElem1))
                setFunction.Remove(valueOfElem1);
            else
                Console.WriteLine("Set od function" + setFunction + " isn't contain function " + valueOfElem1 + "->" + valueOfElem2);
        }
        public void addFunctionTransitionToProtocol(string firstValOfFirstPair, string secondValOfFirstPair,
                                                     string firstValOfSecondPair, string secondValOfSecondPair)
        {
            statesFunctionOfProtocol.Add(new Tuple<string, string>(firstValOfFirstPair, secondValOfFirstPair),
                                         new Tuple<string, string>(firstValOfSecondPair, secondValOfSecondPair));
        }
        public void deleteFunctionTransitionFromProtocol(string firstValOfFirstPair, string secondValOfFirstPair)
        {
            if (statesFunctionOfProtocol.ContainsKey(new Tuple<string, string>(firstValOfFirstPair, secondValOfFirstPair)))
                statesFunctionOfProtocol.Remove(new Tuple<string, string>(firstValOfFirstPair, secondValOfFirstPair));
            else
                Console.WriteLine("This function transition doesn't exist in 'statesFunctionOfProtocol'!");
        }
        public string getResultFunction(Dictionary<string, string> setFunction, string valueOfElement)
        {
            return setFunction[valueOfElement];
        }
        public new Tuple<string, string> getResultFunctionTransition(Dictionary<Tuple<string, string>, Tuple<string, string>> setFunctionTransition,
                                                    string firstValOfFirstPair, string secondValOfFirstPair)
        {
            if (setFunctionTransition.ContainsKey(new Tuple<string, string>(firstValOfFirstPair, secondValOfFirstPair)))
            {
                return setFunctionTransition[new Tuple<string, string>(firstValOfFirstPair, secondValOfFirstPair)];
            }
            return new Tuple<string, string>(firstValOfFirstPair, secondValOfFirstPair);
        }

        //*********************************************************************************************************************************
        // TO DEBUG

        public void showElemOfSet(HashSet<string> set)
        {
            foreach (var elem in set)
            {
                Console.WriteLine(elem);
            }
        }
        public void showSetFunction(Dictionary<string, string> setFunction)
        {
            foreach (var elem in setFunction)
            {
                Console.WriteLine(setFunction);
            }
        }
        public void showSetFunctionTransition(Dictionary<Tuple<string, string>, Tuple<string, string>> setFunctionTransition)
        {
            foreach (var elem in setFunctionTransition)
            {
                Console.WriteLine(setFunctionTransition);
            }
        }
        //**********************************************************************************************************************************


        public bool saveProtocolFromFileToVariable()
        {
            string pathFile = MainWindow.pathToProtocolFile;
            using (StreamReader file = new StreamReader(pathFile))
            {
                try
                {
                    string line;
                    while (!file.EndOfStream)
                    {
                        line = file.ReadLine();
                        if ("<input>" == line)
                        {
                            line = file.ReadLine();
                            while (!line.Contains("</"))
                            {
                                Protocol.Instance.addElementToSet(
                                    Protocol.Instance.inputAlphabetOfProtocol, line);
                                line = file.ReadLine();
                            }
                            //line = file.ReadLine();
                        }
                        if ("<states>" == line)
                        {
                            line = file.ReadLine();
                            while (!line.Contains("</"))
                            {
                                Protocol.Instance.addElementToSet(
                                    Protocol.Instance.statesOfProtocol, line);
                                line = file.ReadLine();
                            }
                            //line = file.ReadLine();
                        }
                        if ("<output>" == line)
                        {
                            line = file.ReadLine();
                            while (!line.Contains("</"))
                            {
                                Protocol.Instance.addElementToSet(
                                    Protocol.Instance.outputAlphabetOfProtocol, line);
                                line = file.ReadLine();
                            }
                        }
                        if ("<inputFunction>" == line)
                        {
                            string symbol1, symbol2;
                            int idx; 

                            line = file.ReadLine();
                            while (!line.Contains("</"))
                            {
                                line = line.Replace("(", "");
                                line = line.Replace(")", "");
                                idx = line.IndexOf("-");
                                symbol1 = line.Substring(0, idx);
                                symbol2 = line.Substring(idx + 2, line.Length - idx - 2);

                                Protocol.Instance.addFunctionToProtocol(
                                    Protocol.Instance.inputFunctionOfProtocol, symbol1, symbol2);
                                line = file.ReadLine();
                            }
                        }
                        if ("<statesFunction>" == line)
                        {
                            string symbol1, symbol2, symbol3, symbol4,
                                firstPairOfSymbols, secondPairOfSymbols;
                            int idx;

                            line = file.ReadLine();
                            while (!line.Contains("</"))
                            {
                                line = line.Replace("(", "");
                                line = line.Replace(")", "");
                                idx = line.IndexOf("-");
                                firstPairOfSymbols = line.Substring(0, idx);
                                secondPairOfSymbols = line.Substring(idx + 2, line.Length - idx - 2);

                                idx = firstPairOfSymbols.IndexOf(",");
                                symbol1 = firstPairOfSymbols.Substring(0, idx);
                                symbol2 = firstPairOfSymbols.Substring(idx + 1 , firstPairOfSymbols.Length - idx - 1);
                                idx = secondPairOfSymbols.IndexOf(",");
                                symbol3 = secondPairOfSymbols.Substring(0, idx);
                                symbol4 = secondPairOfSymbols.Substring(idx + 1, secondPairOfSymbols.Length - idx - 1);
                                                                
                                Protocol.Instance.addFunctionTransitionToProtocol(
                                    symbol1, symbol2, symbol3, symbol4);
                                line = file.ReadLine();
                            }
                        }
                        if ("<outputFunction>" == line)
                        {
                            string symbol1, symbol2;
                            int idx;

                            line = file.ReadLine();
                            while (!line.Contains("</"))
                            {
                                line = line.Replace("(", "");
                                line = line.Replace(")", "");
                                idx = line.IndexOf("-");
                                symbol1 = line.Substring(0, idx);
                                symbol2 = line.Substring(idx + 2, line.Length - idx - 2);

                                Protocol.Instance.addFunctionToProtocol(
                                    Protocol.Instance.outputFunctionOfProtocol, symbol1, symbol2);
                                line = file.ReadLine();
                            }
                        }
                        if ("<typeProtocol>" == line)
                        {
                            if ("lider" == file.ReadLine())
                                ifLiderChooseProtocol = true;
                            else
                                ifLiderChooseProtocol = false;
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Translate protocol - The file could not be read:");
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
    }
}
      

               
                
                    
                    



        /*
        public void addElemToInputAlphabetOfProtocol(string valueOfElement)
        {
            inputAlphabetOfProtocol.Add(valueOfElement);
        }
        public void deleteElemFromInputAlphabetOfProtocol(string valueOfElement)
        {
            if (inputAlphabetOfProtocol.Any())
                inputAlphabetOfProtocol.Remove(valueOfElement);
            else
                Console.WriteLine("Set 'inputAlphabetOfProtocol' is empty!");
        }
        public void addElemToOutputAlphabetOfProtocol(string valueOfElement)
        {
            outputAlphabetOfProtocol.Add(valueOfElement);
        }
        public void deleteElemFromOutputAlphabetOfProtocol(string valueOfElement)
        {
            if (outputAlphabetOfProtocol.Any())
                outputAlphabetOfProtocol.Remove(valueOfElement);
            else
                Console.WriteLine("Set 'outputAlphabetOfProtocol' is empty!");
        }
        public void addElemToStatesOfProtocol(string valueOfElement)
        {
            statesOfProtocol.Add(valueOfElement);
        }
        public void deleteElemFromStatesOfProtocol(string valueOfElement)
        {
            if (statesOfProtocol.Any())
                statesOfProtocol.Remove(valueOfElement);
            else
                Console.WriteLine("Set 'statesOfProtocol' is empty!");
        }
        public void addInputFunctionOfProtocol(string valueOfElement1, string valueOfElement2)
        {
            inputFunctionOfProtocol.Add(valueOfElement1, valueOfElement2);
        }
        public void deleteInputFunctionOfProtocol(string valueOfElement)
        {
            if (inputFunctionOfProtocol.ContainsKey(valueOfElement))
                inputFunctionOfProtocol.Remove(valueOfElement);
            else
                Console.WriteLine("This function doesn't exist in 'inputFunctionOfProtocol'!");
        }
        public void addStatesFunctionOfProtocol
            (string firstValueOfFirstPair, string secondValueOfFirstPair,
            string firstValueOfSecondPair, string secondValueOfSecondPair)
        {
            statesFunctionOfProtocol.Add(new Tuple<string,string> (firstValueOfFirstPair, secondValueOfFirstPair),
                                         new Tuple<string, string> (firstValueOfSecondPair, secondValueOfSecondPair));
        }
        public void deleteStatesFunctionOfProtocol
            (string firstValueOfFirstPair, string secondValueOfFirstPair)
        {
            if (statesFunctionOfProtocol.ContainsKey(new Tuple<string,string> (firstValueOfFirstPair, secondValueOfFirstPair)))
                statesFunctionOfProtocol.Remove(new Tuple<string, string>(firstValueOfFirstPair, secondValueOfFirstPair));
            else
                Console.WriteLine("This function doesn't exist in 'statesFunctionOfProtocol'!");
        }
        public void addOutputFunctionOfProtocol(string valueOfElement1, string valueOfElement2)
        {
            outputFunctionOfProtocol.Add(valueOfElement1, valueOfElement2);
        }
        public void deleteOutputFunctionOfProtocol(string valueOfElement1, string valueOfElement2)
        {
            if (outputFunctionOfProtocol.ContainsKey(valueOfElement1))
                outputFunctionOfProtocol.Remove(valueOfElement1);
            else
                Console.WriteLine("This function doesn't exist in  'outputFunctionOfProtocol'!");
        }
       
        

            std::set<char>::iterator getHandlerToInputAlhabet();

            int getSizeOfInputAlphabet();
            char getResultOfInputFunction(char inputChar);
            char getResultOfOutputFunction(char outputChar);

            void setResultOfProtocol(bool result);

            std::pair<char, char> getResultOfStatesFunction(char stateOfFirstNode, char stateOfSecondNode);
       */
