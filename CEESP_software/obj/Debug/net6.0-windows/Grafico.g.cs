﻿#pragma checksum "..\..\..\Grafico.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CBF8A1CA2A41D022310376B7CECF8A3CF339686D"
//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

using CEESP_software;
using MahApps.Metro;
using MahApps.Metro.Accessibility;
using MahApps.Metro.Actions;
using MahApps.Metro.Automation.Peers;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Converters;
using MahApps.Metro.Markup;
using MahApps.Metro.Theming;
using MahApps.Metro.ValueBoxes;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace CEESP_software {
    
    
    /// <summary>
    /// Grafico
    /// </summary>
    public partial class Grafico : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 102 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Titulo;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Graph;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button refresh;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CBTimes;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TimeSelectet;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btDados;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btLegenda;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Info;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label IaValue;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label VaValue;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label XsIaValue;
        
        #line default
        #line hidden
        
        
        #line 141 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label FPValue;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Phase;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Legenda;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle l;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock l1;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle l2;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock l3;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle l4;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock l5;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle l6;
        
        #line default
        #line hidden
        
        
        #line 157 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock l7;
        
        #line default
        #line hidden
        
        
        #line 158 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle l8;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock l9;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelZoom;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btGraph;
        
        #line default
        #line hidden
        
        
        #line 171 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider Slider;
        
        #line default
        #line hidden
        
        
        #line 172 "..\..\..\Grafico.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.ProgressRing LoadingRing;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CEESP_software;component/grafico.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Grafico.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Titulo = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.Graph = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.refresh = ((System.Windows.Controls.Button)(target));
            
            #line 104 "..\..\..\Grafico.xaml"
            this.refresh.Click += new System.Windows.RoutedEventHandler(this.refresh_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CBTimes = ((System.Windows.Controls.ComboBox)(target));
            
            #line 112 "..\..\..\Grafico.xaml"
            this.CBTimes.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CBTimes_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TimeSelectet = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.btDados = ((System.Windows.Controls.Button)(target));
            
            #line 121 "..\..\..\Grafico.xaml"
            this.btDados.Click += new System.Windows.RoutedEventHandler(this.btDados_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btLegenda = ((System.Windows.Controls.Button)(target));
            
            #line 129 "..\..\..\Grafico.xaml"
            this.btLegenda.Click += new System.Windows.RoutedEventHandler(this.btLegenda_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Info = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.IaValue = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.VaValue = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.XsIaValue = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.FPValue = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.Phase = ((System.Windows.Controls.ComboBox)(target));
            
            #line 143 "..\..\..\Grafico.xaml"
            this.Phase.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Phase_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            this.Legenda = ((System.Windows.Controls.Grid)(target));
            return;
            case 15:
            this.l = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 16:
            this.l1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 17:
            this.l2 = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 18:
            this.l3 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 19:
            this.l4 = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 20:
            this.l5 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 21:
            this.l6 = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 22:
            this.l7 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 23:
            this.l8 = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 24:
            this.l9 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 25:
            this.LabelZoom = ((System.Windows.Controls.Label)(target));
            return;
            case 26:
            this.btGraph = ((System.Windows.Controls.Button)(target));
            
            #line 163 "..\..\..\Grafico.xaml"
            this.btGraph.Click += new System.Windows.RoutedEventHandler(this.btGraph_Click);
            
            #line default
            #line hidden
            return;
            case 27:
            this.Slider = ((System.Windows.Controls.Slider)(target));
            
            #line 171 "..\..\..\Grafico.xaml"
            this.Slider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.Slider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 28:
            this.LoadingRing = ((MahApps.Metro.Controls.ProgressRing)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

