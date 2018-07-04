using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

namespace PopulationProtocolApp
{
    /// <summary>
    /// Interaction logic for ProtocolWindow.xaml
    /// </summary>
    public partial class ProtocolWindow : Window
    {
        public static string pathToProtocolFile { get; private set; }
        public static string protocolFileName { get; private set; }
        public static string protocolDirecory { get; private set; }
        public Window RefToMainForm { get; set; }
        private FolderBrowserDialog folderBrowserDialog;

        public ProtocolWindow()
        {
            InitializeComponent();
            textBox_InputAlphabet.MaxLength = 1;
            textBox_OutputAlphabet.MaxLength = 1;
            textBox_stateAlphabet.MaxLength = 1;
            button_save.IsEnabled = false;
            textBox_protocolDirectory.IsReadOnly = true;
        }

        private bool validateSymbol(string sign)
        {
      
            //todo: ma pozwolić tylko na wielkie i małe litery i cyfry
            return true;
        }

        private bool validateFunction()
        {
            return true;
        }

        private void textBox_InputAlphabet_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox_InputAlphabet.Text == "")
                button_addToInputAlphabet.IsEnabled = false;
            else
                button_addToInputAlphabet.IsEnabled = true;
        }


       
        private void button_addToInputAlphabet_Click_1(object sender, RoutedEventArgs e)
        {
            bool status;
            status = validateSymbol(textBox_InputAlphabet.Text);
           
            if (status)
            {
                if (listBox_inputAlphabet.Items.Contains(this.textBox_InputAlphabet.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Input alphabet already contains this symbol!");
                    textBox_InputAlphabet.Clear();
                    textBox_InputAlphabet.Focus();
                }
                else
                {
                    listBox_inputAlphabet.Items.Add(this.textBox_InputAlphabet.Text);
                    comboBox_FirstElemOfInputFunc.Items.Add(this.textBox_InputAlphabet.Text);
                    textBox_InputAlphabet.Clear();
                    textBox_InputAlphabet.Focus();
                }     
            }
        }

        private void textBox_stateAlphabet_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox_stateAlphabet.Text == "")
                button_addToStatesAlphabet.IsEnabled = false;
            else
                button_addToStatesAlphabet.IsEnabled = true;
        }

        private void textBox_OutputAlphabet_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox_OutputAlphabet.Text == "")
                button_addToOutputAlphabet.IsEnabled = false;
            else
                button_addToOutputAlphabet.IsEnabled = true;
        }

        private void button_addToStatesAlphabet_Click(object sender, RoutedEventArgs e)
        {
            bool status;
            status = validateSymbol(textBox_stateAlphabet.Text);

            if (status)
            {
                if (listBox_stateAlphabet.Items.Contains(this.textBox_stateAlphabet.Text))
                {
                    System.Windows.Forms.MessageBox.Show("States set already contains this symbol!");
                    textBox_stateAlphabet.Clear();
                    textBox_stateAlphabet.Focus();
                }
                else
                {
                    listBox_stateAlphabet.Items.Add(this.textBox_stateAlphabet.Text);
                    comboBox_FirstElemOfStatesFunc1.Items.Add(this.textBox_stateAlphabet.Text);
                    comboBox_FirstElemOfStatesFunc2.Items.Add(this.textBox_stateAlphabet.Text);
                    comboBox_SecondElemOfStatesFunc1.Items.Add(this.textBox_stateAlphabet.Text);
                    comboBox_SecondElemOfStatesFunc2.Items.Add(this.textBox_stateAlphabet.Text);
                    comboBox_FirstElemOfOutpuFunc.Items.Add(this.textBox_stateAlphabet.Text);
                    comboBox1_SecondElemOfInputFunc.Items.Add(this.textBox_stateAlphabet.Text);
                    textBox_stateAlphabet.Clear();
                    textBox_stateAlphabet.Focus();
                }
            }
        }

        private void button_addToOutputAlphabet_Click(object sender, RoutedEventArgs e)
        {
            bool status;
            status = validateSymbol(textBox_OutputAlphabet.Text);

            if (status)
            {
                if (listBox_OutputAlphabet.Items.Contains(this.textBox_OutputAlphabet.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Output alphabet already contains this symbol!");
                    textBox_OutputAlphabet.Clear();
                    textBox_OutputAlphabet.Focus();
                }
                else
                {
                    listBox_OutputAlphabet.Items.Add(this.textBox_OutputAlphabet.Text);
                    comboBox_SecondElemOfOutpuFunc.Items.Add(this.textBox_OutputAlphabet.Text);
                    textBox_OutputAlphabet.Clear();
                    textBox_OutputAlphabet.Focus();
                }
            }
        }

        private void button_removeFromInputAlphabet_Click(object sender, RoutedEventArgs e)
        {
            string s = listBox_inputAlphabet.SelectedItem.ToString();
            listBox_inputAlphabet.Items.Remove(s);
            comboBox_FirstElemOfInputFunc.Items.Remove(s);
        }

        private void button_removeFromStatesAlphabet_Click(object sender, RoutedEventArgs e)
        {
            string s = listBox_stateAlphabet.SelectedItem.ToString();
            listBox_stateAlphabet.Items.Remove(s);
            comboBox_FirstElemOfStatesFunc1.Items.Remove(s);
            comboBox_FirstElemOfStatesFunc2.Items.Remove(s);
            comboBox_SecondElemOfStatesFunc1.Items.Remove(s);
            comboBox_SecondElemOfStatesFunc2.Items.Remove(s);
            comboBox_FirstElemOfOutpuFunc.Items.Remove(s);
            comboBox1_SecondElemOfInputFunc.Items.Remove(s);

            listBox_inputFunction.Items.Clear();
            listBox_StatesFunction.Items.Clear();
            listBox_OutputFunction.Items.Clear();
        }

        private void button_removeFromOutputAlphabet_Click(object sender, RoutedEventArgs e)
        {
            string s = listBox_OutputAlphabet.SelectedItem.ToString();
            listBox_OutputAlphabet.Items.Remove(s);
            comboBox_SecondElemOfOutpuFunc.Items.Remove(s);
            listBox_OutputFunction.Items.Clear();
        }

        private void button_clearInputAlphabet_Click(object sender, RoutedEventArgs e)
        {
            listBox_inputAlphabet.Items.Clear();
            listBox_inputFunction.Items.Clear();
            comboBox_FirstElemOfInputFunc.Items.Clear();
            listBox_inputFunction.Items.Clear();
        }

        private void button_clearStatesAlphabet_Click(object sender, RoutedEventArgs e)
        {
            listBox_stateAlphabet.Items.Clear();
            listBox_inputFunction.Items.Clear();
            listBox_StatesFunction.Items.Clear();
            listBox_OutputFunction.Items.Clear();
            comboBox_FirstElemOfStatesFunc1.Items.Clear();
            comboBox_FirstElemOfStatesFunc2.Items.Clear();
            comboBox_SecondElemOfStatesFunc1.Items.Clear();
            comboBox_SecondElemOfStatesFunc2.Items.Clear();
            comboBox_FirstElemOfOutpuFunc.Items.Clear();
            comboBox1_SecondElemOfInputFunc.Items.Clear();
        }

        private void button_clearOutputAlphabet_Click(object sender, RoutedEventArgs e)
        {
            listBox_OutputAlphabet.Items.Clear();
            listBox_OutputFunction.Items.Clear();
            comboBox_SecondElemOfOutpuFunc.Items.Clear();
        }

        private void button_addInputFunction_Click(object sender, RoutedEventArgs e)
        {
            if ((comboBox_FirstElemOfInputFunc.SelectedItem == null) || (comboBox1_SecondElemOfInputFunc.SelectedItem == null))
            {
                System.Windows.Forms.MessageBox.Show("Both fields of Input Function must be selected!");
            }
            else
            {
                bool status = validateFunction();
                if (status)
                {
                    string x = comboBox_FirstElemOfInputFunc.SelectedItem.ToString();
                    string q = comboBox1_SecondElemOfInputFunc.SelectedItem.ToString();
                    listBox_inputFunction.Items.Add("(" + x + ")->(" + q + ")");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("This Input Function already exist!");
                } 
            }
        }

        private void button_addToStatesFunction_Click(object sender, RoutedEventArgs e)
        {
            if ((comboBox_FirstElemOfStatesFunc1.SelectedItem == null) || (comboBox_SecondElemOfStatesFunc1.SelectedItem == null) || 
                (comboBox_FirstElemOfStatesFunc2.SelectedItem == null) || (comboBox_SecondElemOfStatesFunc2.SelectedItem == null))
            {
                System.Windows.Forms.MessageBox.Show("All fields of Transition Function must be selected!");
            }
            else
            {
                bool status = validateFunction();
                if (status)
                {
                    string q1first = comboBox_FirstElemOfStatesFunc1.SelectedItem.ToString();
                    string q1second = comboBox_SecondElemOfStatesFunc1.SelectedItem.ToString();
                    string q2first = comboBox_FirstElemOfStatesFunc2.SelectedItem.ToString();
                    string q2second = comboBox_SecondElemOfStatesFunc2.SelectedItem.ToString();

                    listBox_StatesFunction.Items.Add("(" + q1first + "," + q1second + ")->(" + q2first + "," + q2second +")");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("This Transition Function already exist!");
                }
            }
        }

        private void button_addToOutpuFunction_Click(object sender, RoutedEventArgs e)
        {
            if ((comboBox_FirstElemOfOutpuFunc.SelectedItem == null) || (comboBox_SecondElemOfOutpuFunc.SelectedItem == null))
            {
                System.Windows.Forms.MessageBox.Show("Both fields of Output Function must be selected!");
            }
            else
            {
                bool status = validateFunction();
                if (status)
                {
                    string q = comboBox_FirstElemOfOutpuFunc.SelectedItem.ToString();
                    string y = comboBox_SecondElemOfOutpuFunc.SelectedItem.ToString();
                    
                    listBox_OutputFunction.Items.Add("(" + q + ")->(" + y + ")");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("This Output Function already exist!");
                }
            }
        }

        private void textBox_protocolPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox_protocolName.Text == "")
                button_save.IsEnabled = false;
            else
            {
                protocolFileName = textBox_protocolName.Text + ".txt";
                button_save.IsEnabled = true;
            }
        }

        private void button_browse_Click(object sender, RoutedEventArgs e)
        {
            using (folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = @"C:\repozytoria\PopulationProtocolMgr\protocols\";
                System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string files = folderBrowserDialog.SelectedPath;
                    textBox_protocolDirectory.Text = files;
                }
            }
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            string fileName = textBox_protocolName.Text;
            string path = textBox_protocolDirectory.Text;
            string pathToProtocolFile = Path.Combine(path + "\\" + fileName + ".txt");
            MainWindow.pathToProtocolFile = pathToProtocolFile;

            createProtocolFile(pathToProtocolFile);

            MainWindow.statusCreateProtocol = true;
            this.RefToMainForm.Show();
            this.Close();           
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            MainWindow.statusCreateProtocol = false;
            this.RefToMainForm.Show();
            this.Close();
        }


        private void button_clearAll_Click(object sender, RoutedEventArgs e)
        {
            listBox_stateAlphabet.Items.Clear();
            listBox_inputFunction.Items.Clear();
            listBox_StatesFunction.Items.Clear();
            listBox_OutputFunction.Items.Clear();
            comboBox_FirstElemOfStatesFunc1.Items.Clear();
            comboBox_FirstElemOfStatesFunc2.Items.Clear();
            comboBox_SecondElemOfStatesFunc1.Items.Clear();
            comboBox_SecondElemOfStatesFunc2.Items.Clear();
            comboBox_FirstElemOfOutpuFunc.Items.Clear();
            comboBox1_SecondElemOfInputFunc.Items.Clear();
            listBox_inputAlphabet.Items.Clear();
            listBox_inputFunction.Items.Clear();
            comboBox_FirstElemOfInputFunc.Items.Clear();
            listBox_OutputAlphabet.Items.Clear();
            listBox_OutputFunction.Items.Clear();
            comboBox_SecondElemOfOutpuFunc.Items.Clear();
            textBox_protocolName.Text = "";
            textBox_protocolDirectory.Text = "";
            MainWindow.pathToProtocolFile = "";
        }

        private void createProtocolFile(string path)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine("<input>");
                foreach (var inputAplhaber in listBox_inputAlphabet.Items)
                {
                    file.WriteLine(inputAplhaber.ToString());
                }
                file.WriteLine("</input>");

                file.WriteLine("<states>");
                foreach (var inputAplhaber in listBox_stateAlphabet.Items)
                {
                    file.WriteLine(inputAplhaber.ToString());
                }
                file.WriteLine("</states>");

                file.WriteLine("<output>");
                foreach (var inputAplhaber in listBox_OutputAlphabet.Items)
                {
                    file.WriteLine(inputAplhaber.ToString());
                }
                file.WriteLine("</output>");

                file.WriteLine("<inputFunction>");
                foreach (var inputAplhaber in listBox_inputFunction.Items)
                {
                    file.WriteLine(inputAplhaber.ToString());
                }
                file.WriteLine("</inputFunction>");

                file.WriteLine("<statesFunction>");
                foreach (var inputAplhaber in listBox_StatesFunction.Items)
                {
                    file.WriteLine(inputAplhaber.ToString());
                }
                file.WriteLine("</statesFunction>");

                file.WriteLine("<outputFunction>");
                foreach (var inputAplhaber in listBox_OutputFunction.Items)
                {
                    file.WriteLine(inputAplhaber.ToString());
                }
                file.WriteLine("</outputFunction>");
            }
        }

        private void textBox_InputAlphabet_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //char aa = (char)e.Key;

            //bool bb = Char.IsDigit(aa);

            //if (Char.IsLetter((char) e.Key) || Char.IsNumber((char) e.Key))
            //    e.Handled = true;
            //else
            //    e.Handled = false;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //private void textBox_InputAlphabet_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    if (!(char.IsLetter(e.KeyCode) || (char.IsNumber(e.KeyCode))))
        //        e.Handled = true;
        //    else
        //        System.Windows.MessageBox.Show("The symbol must be a letter or number.");
        //}
    }
}
