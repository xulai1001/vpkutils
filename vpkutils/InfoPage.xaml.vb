Imports System.IO
Imports Microsoft.Win32
Imports System.Collections.ObjectModel
Imports vpkutils.Worker
Imports ICSharpCode.SharpZipLib
Imports System.Security

Class InfoPage

    Private log As NLog.Logger = NLog.LogManager.GetCurrentClassLogger()

    Private m_InfoPageViewModel As CompressionPrarameterViewModel
    Private m_worker As New VPKWorker()
    Public root As Frame

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Initialize()
    End Sub

    Public Sub New(ByVal fileNames() As String)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Initialize()
        Me.m_InfoPageViewModel.Files = New ObservableCollection(Of String)(fileNames)
        Dim fi As New FileInfo(fileNames(0))
        Me.m_InfoPageViewModel.OutText = Path.Combine(fi.DirectoryName, fi.Directory.Name & ".vpk")
    End Sub

    Private Sub Initialize()
        Me.m_InfoPageViewModel = New CompressionPrarameterViewModel()
        Me.DataContext = Me.m_InfoPageViewModel
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Keyboard.Focus(txt_pwd)
        Me.ShowsNavigationUI = False
    End Sub

    Private Sub btn_src_open_Click(sender As Object, e As RoutedEventArgs)
        Using ofd = New System.Windows.Forms.OpenFileDialog()
            With ofd
                .Multiselect = True
                .Title = "Open file"
                .Filter = "All files (*.*)|*.*"
                If .ShowDialog() = Forms.DialogResult.OK Then
                    Me.m_InfoPageViewModel.Files = New ObservableCollection(Of String)(.FileNames)
                    Dim fi As New FileInfo(ofd.FileNames(0))
                    Me.m_InfoPageViewModel.OutText = Path.Combine(fi.DirectoryName, fi.Directory.Name & ".vpk")
                End If
            End With
        End Using
    End Sub

    Private Sub btn_out_open_Click(sender As Object, e As RoutedEventArgs)
        Using sfd = New System.Windows.Forms.SaveFileDialog()
            With sfd
                .Title = "Save file"
                .Filter = "Victor Pack(*.vpk)|*.vpk"
                If .ShowDialog() = Forms.DialogResult.OK Then
                    Me.m_InfoPageViewModel.OutText = .FileName
                End If
            End With
        End Using
    End Sub

    Private Sub txt_out_TextChanged(sender As Object, e As TextChangedEventArgs)
        Me.m_InfoPageViewModel.OutText = Me.txt_out.Text
    End Sub

    Private Sub btn_toggle_Click(sender As Object, e As RoutedEventArgs)
        MessageBox.Show(Me.txt_pwd.Password, "当前口令")
    End Sub

    ''' <summary>
    ''' ToDo: Compelete the compress function.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Btn_Go_Click(sender As Object, e As RoutedEventArgs)
        Me.m_worker.Prarmeters = New Parameter(
            Me.m_InfoPageViewModel.OutText,
            Me.m_InfoPageViewModel.Files.ToArray()) With
            {.Password = Me.txt_pwd.Password}
        m_worker.Compress(m_InfoPageViewModel)
        Me.NavigationService.Navigate(New ProgressPage(m_worker, m_InfoPageViewModel))
    End Sub

    ''' <summary>
    ''' Well, the SecuritySafeCriticalAttribute is very important for invoking native Win32 API...
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    <SecuritySafeCritical>
    Private Sub Btn_SrcView_Click(sender As Object, e As RoutedEventArgs)
        Dim f As ObservableCollection(Of String) = m_InfoPageViewModel.Files
        If f.Count = 1 And (Path.GetExtension(f(0)) = ".zip" Or Path.GetExtension(f(0)) = ".vpk") Then
            m_worker.Param.Files = f.ToArray
            root.NavigationUIVisibility = NavigationUIVisibility.Visible
            Me.NavigationService.Navigate(New ArchivePage(m_worker, m_InfoPageViewModel))
        End If
    End Sub

End Class