﻿#pragma checksum "..\..\SearchPolicyForCustomers.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7F99E35656CE7511F3863CCB68763F9E78DAE34331D3A53463616CB9B5A59E62"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PESPresentationLayer;
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


namespace PESPresentationLayer {
    
    
    /// <summary>
    /// SearchPolicyForCustomers
    /// </summary>
    public partial class SearchPolicyForCustomers : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 106 "..\..\SearchPolicyForCustomers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_searchPolicyHead;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\SearchPolicyForCustomers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_policyNumber;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\SearchPolicyForCustomers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_policyNumber;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\SearchPolicyForCustomers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_searchPolicy;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\SearchPolicyForCustomers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid grid_policyDetails;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\SearchPolicyForCustomers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_back;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\SearchPolicyForCustomers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_reload;
        
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
            System.Uri resourceLocater = new System.Uri("/PESPresentationLayer;component/searchpolicyforcustomers.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SearchPolicyForCustomers.xaml"
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
            
            #line 9 "..\..\SearchPolicyForCustomers.xaml"
            ((PESPresentationLayer.SearchPolicyForCustomers)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txt_searchPolicyHead = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.lbl_policyNumber = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.txt_policyNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btn_searchPolicy = ((System.Windows.Controls.Button)(target));
            
            #line 110 "..\..\SearchPolicyForCustomers.xaml"
            this.btn_searchPolicy.Click += new System.Windows.RoutedEventHandler(this.btn_searchPolicy_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.grid_policyDetails = ((System.Windows.Controls.DataGrid)(target));
            
            #line 113 "..\..\SearchPolicyForCustomers.xaml"
            this.grid_policyDetails.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.grid_policyDetails_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_back = ((System.Windows.Controls.Button)(target));
            
            #line 126 "..\..\SearchPolicyForCustomers.xaml"
            this.btn_back.Click += new System.Windows.RoutedEventHandler(this.btn_back_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btn_reload = ((System.Windows.Controls.Button)(target));
            
            #line 127 "..\..\SearchPolicyForCustomers.xaml"
            this.btn_reload.Click += new System.Windows.RoutedEventHandler(this.btn_reload_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

