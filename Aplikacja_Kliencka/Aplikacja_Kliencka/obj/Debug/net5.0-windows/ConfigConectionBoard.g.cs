#pragma checksum "..\..\..\ConfigConectionBoard.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B4E3C844D51947B8C2029BED34BB44EF48881F94"
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


namespace Aplikacja_Kliencka {
    
    
    /// <summary>
    /// ConfigConectionBoard
    /// </summary>
    public partial class ConfigConectionBoard : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\ConfigConectionBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_userName;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\ConfigConectionBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_password;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\ConfigConectionBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_vHostName;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\ConfigConectionBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_hostName;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\ConfigConectionBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_port;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\ConfigConectionBoard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_queue_res;
        
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
            System.Uri resourceLocater = new System.Uri("/Aplikacja_Kliencka;component/configconectionboard.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ConfigConectionBoard.xaml"
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
            this.txt_userName = ((System.Windows.Controls.TextBox)(target));
            
            #line 11 "..\..\..\ConfigConectionBoard.xaml"
            this.txt_userName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txt_password = ((System.Windows.Controls.TextBox)(target));
            
            #line 13 "..\..\..\ConfigConectionBoard.xaml"
            this.txt_password.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txt_vHostName = ((System.Windows.Controls.TextBox)(target));
            
            #line 15 "..\..\..\ConfigConectionBoard.xaml"
            this.txt_vHostName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txt_hostName = ((System.Windows.Controls.TextBox)(target));
            
            #line 17 "..\..\..\ConfigConectionBoard.xaml"
            this.txt_hostName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txt_port = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\..\ConfigConectionBoard.xaml"
            this.txt_port.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txt_queue_res = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\..\ConfigConectionBoard.xaml"
            this.txt_queue_res.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

