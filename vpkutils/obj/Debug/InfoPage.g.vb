﻿#ExternalChecksum("..\..\InfoPage.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","078957340D2992546EF7E0385F8D9E78")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.33440
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Media.TextFormatting
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Shell
Imports vpkutils


'''<summary>
'''InfoPage
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class InfoPage
    Inherits System.Windows.Controls.Page
    Implements System.Windows.Markup.IComponentConnector
    
    
    #ExternalSource("..\..\InfoPage.xaml",48)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents txt_src As System.Windows.Controls.TextBlock
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\InfoPage.xaml",49)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents txt_out As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\InfoPage.xaml",50)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents txt_pwd As System.Windows.Controls.PasswordBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\InfoPage.xaml",55)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btn_src_open As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\InfoPage.xaml",56)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btn_src_view As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\InfoPage.xaml",57)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btn_out_open As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\InfoPage.xaml",58)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btn_go As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\InfoPage.xaml",59)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btn_toggle As System.Windows.Controls.Button
    
    #End ExternalSource
    
    Private _contentLoaded As Boolean
    
    '''<summary>
    '''InitializeComponent
    '''</summary>
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")>  _
    Public Sub InitializeComponent() Implements System.Windows.Markup.IComponentConnector.InitializeComponent
        If _contentLoaded Then
            Return
        End If
        _contentLoaded = true
        Dim resourceLocater As System.Uri = New System.Uri("/vpkutils;component/infopage.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\InfoPage.xaml",1)
        System.Windows.Application.LoadComponent(Me, resourceLocater)
        
        #End ExternalSource
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")>  _
    Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
        If (connectionId = 1) Then
            
            #ExternalSource("..\..\InfoPage.xaml",8)
            AddHandler CType(target,InfoPage).Loaded, New System.Windows.RoutedEventHandler(AddressOf Me.Page_Loaded)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 2) Then
            Me.txt_src = CType(target,System.Windows.Controls.TextBlock)
            Return
        End If
        If (connectionId = 3) Then
            Me.txt_out = CType(target,System.Windows.Controls.TextBox)
            Return
        End If
        If (connectionId = 4) Then
            Me.txt_pwd = CType(target,System.Windows.Controls.PasswordBox)
            Return
        End If
        If (connectionId = 5) Then
            Me.btn_src_open = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\InfoPage.xaml",55)
            AddHandler Me.btn_src_open.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btn_src_open_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 6) Then
            Me.btn_src_view = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\InfoPage.xaml",56)
            AddHandler Me.btn_src_view.Click, New System.Windows.RoutedEventHandler(AddressOf Me.Btn_SrcView_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 7) Then
            Me.btn_out_open = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\InfoPage.xaml",57)
            AddHandler Me.btn_out_open.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btn_out_open_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 8) Then
            Me.btn_go = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\InfoPage.xaml",58)
            AddHandler Me.btn_go.Click, New System.Windows.RoutedEventHandler(AddressOf Me.Btn_Go_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 9) Then
            Me.btn_toggle = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\InfoPage.xaml",59)
            AddHandler Me.btn_toggle.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btn_toggle_Click)
            
            #End ExternalSource
            Return
        End If
        Me._contentLoaded = true
    End Sub
End Class
