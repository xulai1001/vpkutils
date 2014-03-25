Imports System.IO
Imports System.ComponentModel
Imports System.Collections.ObjectModel

''' <summary>
''' The View Model for showing the compression parameters. 
''' </summary>
''' <remarks></remarks>
Public Class CompressionPrarameterViewModel
    Inherits Bindable

    Private m_SrcText As String
    Private m_OutText As String

    Private WithEvents m_Files As New ObservableCollection(Of String)()

    Public Property Files() As ObservableCollection(Of String)
        Get
            Return Me.m_Files
        End Get
        Set(value As ObservableCollection(Of String))
            Me.m_Files.Clear()
            For Each item In value
                Me.m_Files.Add(item)
            Next
        End Set
    End Property

    ''' <summary>
    ''' The view form of the input file list.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SrcText() As String
        Get
            Return Me.m_SrcText
        End Get
        Set(value As String)
            SetProperty(m_SrcText, value)
        End Set
    End Property

    Public Property OutText() As String
        Get
            Return Me.m_OutText
        End Get
        Set(value As String)
            SetProperty(m_OutText, value)
        End Set
    End Property

    Private m_curr_file As String
    Private m_curr_index As Integer
    Private m_count As Integer
    Public Property CurrFile As String
        Get
            Return m_curr_file
        End Get
        Set(value As String)
            SetProperty(m_curr_file, value)
        End Set
    End Property

    Public Property CurrIndex As Integer
        Get
            Return m_curr_index
        End Get
        Set(value As Integer)
            SetProperty(m_curr_index, value)
        End Set
    End Property

    Public Property CurrCount As Integer
        Get
            Return m_count
        End Get
        Set(value As Integer)
            SetProperty(m_count, value)
        End Set
    End Property

    Public Sub New()
        Me.m_SrcText = ""
        Me.m_OutText = ""
    End Sub

    Public Sub New(ByVal src As String, ByVal out As String)
        Me.m_SrcText = src
        Me.m_OutText = out
    End Sub

    Private Sub m_Files_CollectionChanged(sender As Object, e As Specialized.NotifyCollectionChangedEventArgs) Handles m_Files.CollectionChanged
        If Me.m_Files.Count > 0 Then
            If Me.m_Files.Count = 1 Then
                Me.SrcText = Me.m_Files.First()
            Else
                Me.SrcText = String.Format("{0} ... ({1} files)", Me.m_Files.First(), Me.m_Files.Count)
            End If
        Else
            Me.SrcText = ""
        End If
    End Sub

End Class