<<<<<<< HEAD
﻿#pragma checksum "..\..\..\WinMenuMozo.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9DA7ABA21AC72E2140562B9BEC638CFBAC66F70FF4E51FDE7146B6A16518D28B"
=======
﻿#pragma checksum "..\..\..\WinMenuMozo.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D0E9A4C189CEB768970FC82F3B8B6CD1"
>>>>>>> 08a88520d5b9dbe8d9de57a1c4666f0eebbfdf82
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace LPOO2_GRUPO_08 {
    
    
    /// <summary>
    /// WinMenuMozo
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class WinMenuMozo : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\WinMenuMozo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu menu1;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\WinMenuMozo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuCliente;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\WinMenuMozo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuMesas;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\WinMenuMozo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuPedidos;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/LPOO2_GRUPO_08;component/winmenumozo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WinMenuMozo.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.menu1 = ((System.Windows.Controls.Menu)(target));
            return;
            case 2:
            this.menuCliente = ((System.Windows.Controls.MenuItem)(target));
            
            #line 7 "..\..\..\WinMenuMozo.xaml"
            this.menuCliente.Click += new System.Windows.RoutedEventHandler(this.menuCliente_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.menuMesas = ((System.Windows.Controls.MenuItem)(target));
            
            #line 8 "..\..\..\WinMenuMozo.xaml"
            this.menuMesas.Click += new System.Windows.RoutedEventHandler(this.menuMesas_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.menuPedidos = ((System.Windows.Controls.MenuItem)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

