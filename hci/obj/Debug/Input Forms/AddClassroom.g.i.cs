﻿#pragma checksum "..\..\..\Input Forms\AddClassroom.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8B790E6C6EF384AC1F68A49128047F7B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using hci.Commands;
using hci.Input_Forms;


namespace hci.Input_Forms {
    
    
    /// <summary>
    /// AddClassroom
    /// </summary>
    public partial class AddClassroom : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 41 "..\..\..\Input Forms\AddClassroom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox classroomID;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Input Forms\AddClassroom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox description;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Input Forms\AddClassroom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox size;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\Input Forms\AddClassroom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox projector;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Input Forms\AddClassroom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox board;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\Input Forms\AddClassroom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox smartboard;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\Input Forms\AddClassroom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox os;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\Input Forms\AddClassroom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox softwares;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\Input Forms\AddClassroom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Ok;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\Input Forms\AddClassroom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Cancel;
        
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
            System.Uri resourceLocater = new System.Uri("/hci;component/input%20forms/addclassroom.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Input Forms\AddClassroom.xaml"
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
            
            #line 11 "..\..\..\Input Forms\AddClassroom.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.ClassroomAdded_CanExecute);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\Input Forms\AddClassroom.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.ClassroomAdded_Executed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 12 "..\..\..\Input Forms\AddClassroom.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.ClassroomClose_CanExecute);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\Input Forms\AddClassroom.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.ClassroomClose_Executed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.classroomID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.description = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.size = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.projector = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.board = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.smartboard = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.os = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.softwares = ((System.Windows.Controls.ComboBox)(target));
            
            #line 99 "..\..\..\Input Forms\AddClassroom.xaml"
            this.softwares.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.OnSftwObjectsSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Ok = ((System.Windows.Controls.Button)(target));
            return;
            case 13:
            this.Cancel = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 11:
            
            #line 104 "..\..\..\Input Forms\AddClassroom.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.OnSftwObjectCheckBoxChecked);
            
            #line default
            #line hidden
            
            #line 105 "..\..\..\Input Forms\AddClassroom.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.OnContentChanged);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

