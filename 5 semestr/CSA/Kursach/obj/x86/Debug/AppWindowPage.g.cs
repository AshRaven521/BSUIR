﻿#pragma checksum "C:\PTUIR\5 semestr\ABC\uwp_test\AppWindowPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C25DCDC227FAC4202AF361E94FB06C43"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace uwp_test
{
    partial class AppWindowPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // AppWindowPage.xaml line 15
                {
                    this.MapControl1 = (global::Windows.UI.Xaml.Controls.Maps.MapControl)(target);
                }
                break;
            case 3: // AppWindowPage.xaml line 29
                {
                    this.cluster = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.cluster).SelectionChanged += this.cluster_SelectionChanged;
                }
                break;
            case 4: // AppWindowPage.xaml line 38
                {
                    this.pointNumberTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // AppWindowPage.xaml line 50
                {
                    this.viewTypeComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 6: // AppWindowPage.xaml line 58
                {
                    this.applyButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.applyButton).Click += this.applyButton_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
