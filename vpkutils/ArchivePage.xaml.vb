Imports vpkutils
Imports vpkutils.Worker
Imports ICSharpCode.SharpZipLib.Zip

Class ArchivePage

    Private m_worker As VPKWorker
    Private m_view As CompressionPrarameterViewModel
    Public Sub New(w As VPKWorker, v As CompressionPrarameterViewModel)
        InitializeComponent()
        m_worker = w
        m_view = v
        Dim item = m_worker.GetEntries()
        For Each i In item
            lv_file.Items.Add(i)
        Next
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Me.ShowsNavigationUI = True
    End Sub
End Class
