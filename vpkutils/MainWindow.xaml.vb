Imports System.IO
Imports System.Windows.Forms

Class MainWindow

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub NavigateToInfoPage(ByVal files() As String)
        Dim infopage As New InfoPage(files)
        infopage.root = frame
        'Me.frame.Visibility = Windows.Visibility.Visible
        Me.btn_load.IsEnabled = False
        Me.frame.Navigate(infopage)
        'frame.Navigate(New TipPage)
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Using ofd As New OpenFileDialog()
            With ofd
                .Multiselect = True
                If .ShowDialog() = Forms.DialogResult.OK Then
                    Me.NavigateToInfoPage(.FileNames)
                End If
            End With
        End Using
    End Sub

    Private Sub btn_load_DragEnter(sender As Object, e As Windows.DragEventArgs) Handles btn_load.DragEnter
        e.Effects = Windows.DragDropEffects.Copy
        If e.Data.GetDataPresent(Windows.DataFormats.FileDrop) Then
            Dim data = e.Data.GetData(Windows.DataFormats.FileDrop)
            If TypeOf (data) Is String() Then
                Dim files = CType(data, String())
                If files.Length > 1 Then
                    btn_load.Content = Path.GetFileName(files(0)) & " ... (" & files.Length.ToString & " files)"
                Else
                    btn_load.Content = files(0)
                End If

            End If
        End If
    End Sub

    Private Sub btn_load_DragLeave(sender As Object, e As Windows.DragEventArgs) Handles btn_load.DragLeave
        e.Effects = Windows.DragDropEffects.None
        'btn_load.Content = "Drag to pack"
    End Sub

    Private Sub btn_load_Drop(sender As Object, e As Windows.DragEventArgs) Handles btn_load.Drop
        Dim data = e.Data.GetData(Windows.DataFormats.FileDrop)
        If TypeOf data Is String() Then
            Dim files = CType(data, String())
            Me.NavigateToInfoPage(files)
        End If
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        frame.Navigate(New TipPage())
    End Sub
End Class