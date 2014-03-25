Imports vpkutils
Imports vpkutils.Worker
Imports ICSharpCode.SharpZipLib.Zip

Class ProgressPage

    Private m_worker As VPKWorker
    Private m_view As CompressionPrarameterViewModel
    Public Sub New(w As VPKWorker, v As CompressionPrarameterViewModel)
        InitializeComponent()
        m_worker = w
        m_view = v
        Me.DataContext = m_view
    End Sub
End Class