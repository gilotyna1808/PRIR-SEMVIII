﻿#pragma checksum "..\..\..\ClientWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FEF202E7E39382C08E7F9FA5253D86DA467EFDCA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using Aplikacja_Kliencka;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Aplikacja_Kliencka {
    
    
    /// <summary>
    /// ClientWindow
    /// </summary>
    public partial class ClientWindow : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\ClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_nazwa;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\ClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_start;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\ClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_log;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\ClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_stop;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\ClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_Status;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\ClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lbl_Zadanie;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Aplikacja_Kliencka;component/clientwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ClientWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lbl_nazwa = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.btn_start = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\ClientWindow.xaml"
            this.btn_start.Click += new System.Windows.RoutedEventHandler(this.btn_start_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_log = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\ClientWindow.xaml"
            this.btn_log.Click += new System.Windows.RoutedEventHandler(this.btn_log_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_stop = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\ClientWindow.xaml"
            this.btn_stop.Click += new System.Windows.RoutedEventHandler(this.btn_stop_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lbl_Status = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.lbl_Zadanie = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
