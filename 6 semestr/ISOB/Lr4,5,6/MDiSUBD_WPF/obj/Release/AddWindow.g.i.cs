﻿#pragma checksum "..\..\AddWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "81D68E5B7FDA9F7A4F73CB0F6B8BF7F7A5071CBC47489C4C6B57764DE8DF704D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MDiSUBD_WPF;
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


namespace MDiSUBD_WPF {
    
    
    /// <summary>
    /// AddWindow
    /// </summary>
    public partial class AddWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\AddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox customerComboBox;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\AddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addOrderButton;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\AddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox stationComboBox;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\AddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox detailComboBox;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\AddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox masterComboBox;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\AddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox workComboBox;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\AddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox paymentComboBox;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\AddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox photoComboBox;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\AddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox orderNumberTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/MDiSUBD_WPF;component/addwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddWindow.xaml"
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
            
            #line 8 "..\..\AddWindow.xaml"
            ((MDiSUBD_WPF.AddWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.customerComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.addOrderButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\AddWindow.xaml"
            this.addOrderButton.Click += new System.Windows.RoutedEventHandler(this.addOrderButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.stationComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.detailComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.masterComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.workComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.paymentComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.photoComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.orderNumberTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

