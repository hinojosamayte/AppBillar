﻿#pragma checksum "C:\Users\Mayte\Desktop\Billar\GestioTorneig\Pages\TorneoPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3A650724CF5E35295FBB51D2B793F141"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestioTorneig.Pages
{
    partial class TorneoPage : 
        global::Windows.UI.Xaml.Controls.Page, 
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
                    this.gridGrups = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 2:
                {
                    this.gridAgenda = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3:
                {
                    this.tbErrorTorneo = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4:
                {
                    this.dpDataini = (global::Windows.UI.Xaml.Controls.CalendarDatePicker)(target);
                }
                break;
            case 5:
                {
                    this.tbTitol = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6:
                {
                    this.cboxModalitat = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 7:
                {
                    this.btnAddTorneo = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 94 "..\..\..\Pages\TorneoPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnAddTorneo).Click += this.btnAddTorneo_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.btnCloseTorneo = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 95 "..\..\..\Pages\TorneoPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnCloseTorneo).Click += this.btnCloseTorneo_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.lvSTorneos = (global::GestioTorneig.Controls.Control_ListViewDBCreator)(target);
                }
                break;
            case 10:
                {
                    this.lvSInscrits = (global::GestioTorneig.Controls.Control_ListViewDBCreator)(target);
                }
                break;
            case 11:
                {
                    this.txtnom = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 12:
                {
                    this.txtcCaram = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 13:
                {
                    this.txtEntrades = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 14:
                {
                    this.AddGroup = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 59 "..\..\..\Pages\TorneoPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddGroup).Click += this.AddGroup_Click;
                    #line default
                }
                break;
            case 15:
                {
                    this.CloseGroup = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 60 "..\..\..\Pages\TorneoPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.CloseGroup).Click += this.CloseGroup_Click;
                    #line default
                }
                break;
            case 16:
                {
                    this.tbMissatgeError = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 17:
                {
                    this.btnNou = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 18:
                {
                    this.btnEditar = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 19:
                {
                    this.btnEsborrar = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 20:
                {
                    this.lvTorneos = (global::GestioTorneig.Controls.Control_ListViewDBCreator)(target);
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
