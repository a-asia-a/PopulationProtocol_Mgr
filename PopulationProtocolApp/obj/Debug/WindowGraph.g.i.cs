﻿#pragma checksum "..\..\WindowGraph.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8F7CDFA062A82F31E57C17B2CBC238E774278A83"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PopulationProtocolApp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PopulationProtocolApp {
    
    
    /// <summary>
    /// WindowGraph
    /// </summary>
    public partial class WindowGraph : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\WindowGraph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labNumberOfNodes;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\WindowGraph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBxNumberOfNodes;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\WindowGraph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox groupBxDefineGraph;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\WindowGraph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBxNumberOfSymbol;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\WindowGraph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAdd;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\WindowGraph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listNode;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\WindowGraph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRemove;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\WindowGraph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEdit;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\WindowGraph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbBxInputAlphabet;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\WindowGraph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkBxIfRandom;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\WindowGraph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClear;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\WindowGraph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOk;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PopulationProtocolApp;component/windowgraph.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\WindowGraph.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\WindowGraph.xaml"
            ((PopulationProtocolApp.WindowGraph)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.labNumberOfNodes = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.txtBxNumberOfNodes = ((System.Windows.Controls.TextBox)(target));
            
            #line 17 "..\..\WindowGraph.xaml"
            this.txtBxNumberOfNodes.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtBxNumberOfNodes_TextChanged);
            
            #line default
            #line hidden
            
            #line 17 "..\..\WindowGraph.xaml"
            this.txtBxNumberOfNodes.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.txtBxNumberOfNodes_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.groupBxDefineGraph = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 5:
            this.txtBxNumberOfSymbol = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\WindowGraph.xaml"
            this.txtBxNumberOfSymbol.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.txtBxNumberOfSymbol_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnAdd = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\WindowGraph.xaml"
            this.btnAdd.Click += new System.Windows.RoutedEventHandler(this.btnAdd_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.listNode = ((System.Windows.Controls.ListView)(target));
            return;
            case 8:
            this.btnRemove = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\WindowGraph.xaml"
            this.btnRemove.Click += new System.Windows.RoutedEventHandler(this.btnRemove_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnEdit = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.cmbBxInputAlphabet = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            this.checkBxIfRandom = ((System.Windows.Controls.CheckBox)(target));
            
            #line 44 "..\..\WindowGraph.xaml"
            this.checkBxIfRandom.Checked += new System.Windows.RoutedEventHandler(this.checkBxIfRandom_Checked);
            
            #line default
            #line hidden
            
            #line 44 "..\..\WindowGraph.xaml"
            this.checkBxIfRandom.Unchecked += new System.Windows.RoutedEventHandler(this.checkBxIfRandom_Unchecked);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnClear = ((System.Windows.Controls.Button)(target));
            return;
            case 13:
            this.btnOk = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\WindowGraph.xaml"
            this.btnOk.Click += new System.Windows.RoutedEventHandler(this.btnOk_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

