﻿#pragma checksum "C:\Users\Mayte\Desktop\Billar\GestioTorneig\Controls\Control_ListViewDBCreator.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EE6B701D1EF6E3CF40510E9440B94800"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestioTorneig.Controls
{
    partial class Control_ListViewDBCreator : 
        global::Windows.UI.Xaml.Controls.UserControl, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.lvGeneric = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 20 "..\..\..\Controls\Control_ListViewDBCreator.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.lvGeneric).SelectionChanged += this.lvGeneric_SelectionChanged;
                    #line 20 "..\..\..\Controls\Control_ListViewDBCreator.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.lvGeneric).DoubleTapped += this.lvGeneric_DoubleTapped;
                    #line default
                }
                break;
            case 2:
                {
                    this.borderEmpty = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
