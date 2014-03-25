Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO
Imports System.IO.Compression

Namespace Worker

    Public Class Parameter

        Public Property Files() As String()
        Public Property OutputPath() As String
        Public Property Password() As String
        Public Property CompressionMode() As CompressionMode

        Public Sub New()
            Me.Files = New String() {}
            Me.OutputPath = ""
            Me.Password = ""
            Me.CompressionMode = CompressionMode.Compress
        End Sub

        ''' <summary>
        ''' Choose a copmpression mode and pass the input and output paths.
        ''' </summary>
        ''' <param name="mode"></param>
        ''' <param name="outputPath">
        ''' When it comes to Compress mode, this must be a compressed file path.
        ''' When it comes to Decompress mode, this must be a output directory path.
        ''' </param>
        ''' <param name="files">
        ''' </param>
        ''' <remarks></remarks>
        Public Sub New(ByVal mode As CompressionMode, ByVal outputPath As String, ByVal ParamArray files() As String)
            Me.CompressionMode = mode
            If Me.CompressionMode = IO.Compression.CompressionMode.Decompress Then
                If Not Directory.Exists(outputPath) Then
                    Throw New ArgumentException("For Decompress mode, this must be a output directory path.")
                End If
            End If
            Me.Files = files
            Me.OutputPath = outputPath
        End Sub

        Public Sub New(ByVal outputPath As String, ByVal ParamArray filesToCompress() As String)
            Me.Files = filesToCompress
            Me.OutputPath = outputPath
        End Sub

    End Class

End Namespace