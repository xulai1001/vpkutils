Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip

Namespace Worker

    ''' <summary>
    ''' The actual worker core.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class VPKWorker

        Private log As NLog.Logger = NLog.LogManager.GetCurrentClassLogger()

        Public Property Prarmeters() As Parameter
        Public ReadOnly Property Param() As Parameter
            Get
                Return Me.Prarmeters
            End Get
        End Property
        Public Sub New()
            Me.Prarmeters = New Parameter()
        End Sub

        Public Sub New(ByVal param As Parameter)
            Me.Prarmeters = param
        End Sub

        ''' <summary>
        ''' Compress files to a VPK file.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Delegate Sub MakeZipDelegate(out As String, lv As Integer, pwd As String, files As String(), vm As CompressionPrarameterViewModel)
        Public Function Compress(Optional vm As CompressionPrarameterViewModel = Nothing) As Boolean
            Try
                Dim func As New MakeZipDelegate(AddressOf MakeZipFile)
                func.BeginInvoke(Me.Prarmeters.OutputPath, 5, Me.Prarmeters.Password, Me.Prarmeters.Files.ToArray(), vm,
                                 New AsyncCallback(Sub() MessageBox.Show("Compress Complete!")), Nothing)
                Return True
            Catch ex As Exception
                log.Error(ex)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Extract files from a VPK.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Decompress() As Boolean
            Try
                Dim di As New DirectoryInfo(Me.Prarmeters.OutputPath)
                If di.Exists Then
                    Using fs As New FileStream(Me.Prarmeters.Files.First(), FileMode.Open)
                        Return ExtractFromStream(fs, Me.Prarmeters.OutputPath)
                    End Using
                Else
                    Return False
                End If
            Catch ex As Exception
                log.Error(ex)
                Return False
            End Try
        End Function

        Public Function GetEntries() As ZipEntry()
            If Path.GetExtension(Param.Files(0)) = ".zip" Or Path.GetExtension(Param.Files(0)) = ".vpk" Then
                Dim zf As ZipFile = New ZipFile(Param.Files(0))
                Dim retvar As List(Of ZipEntry) = New List(Of ZipEntry)
                Dim i = 0
                For i = 0 To zf.Count - 1
                    retvar.Add(zf.EntryByIndex(i))
                Next
                Return retvar.ToArray
            End If
        End Function
    End Class

End Namespace