Imports System.Threading.Tasks
Imports System.Text
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip

''' <summary>
''' Moved from Runa.Common.Helper.
''' </summary>
''' <remarks></remarks>
Public Module ModuleHelper

    ''' <summary>
    ''' Normalize the path spliter with '/'.
    ''' </summary>
    ''' <param name="p"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNomalizedPath(ByVal p As String) As String
        Dim replace As New System.Text.RegularExpressions.Regex("(/|\\)+")
        Return replace.Replace(p, "/")
    End Function

    Public Function GetRelativePath(ByVal sourcePath As String, ByVal targetBase As String) As String
        Dim replace As New System.Text.RegularExpressions.Regex("(/|\\)+")
#If NETFX_CORE Then
        targetBase = replace.Replace(targetBase, "/")
        sourcePath = replace.Replace(sourcePath, "/")
#Else
        targetBase = replace.Replace(Path.GetFullPath(targetBase), "/")
        sourcePath = replace.Replace(Path.GetFullPath(sourcePath), "/")
#End If
        If sourcePath.EndsWith("/") = False Then
            sourcePath = sourcePath.Substring(0, sourcePath.LastIndexOf("/"c) + 1)
        End If
        Dim p As String() = sourcePath.Split(New Char() {"/"c}, StringSplitOptions.RemoveEmptyEntries)
        Dim t As String() = targetBase.Split(New Char() {"/"c}, StringSplitOptions.RemoveEmptyEntries)
        Dim len As Integer = Math.Min(p.Length, t.Length)
        Dim same As Integer = 0
        While same < len
            If p(same) <> t(same) Then
                Exit While
            End If
            same += 1
        End While
        If same = 0 Then
            Return targetBase
        End If
        Dim sb As New StringBuilder("./")
        For i As Integer = same To p.Length - 1
            sb.Append("../")
        Next
        For i As Integer = same To t.Length - 1
            sb.Append("/"c).Append(t(i))
        Next
        If targetBase.EndsWith("/") Then
            sb.Insert(2, "../")
            sb.Append("/"c)
        End If
        Return replace.Replace(sb.ToString(), "/")
    End Function

    Public Function GetCommonParentDir(ByVal ParamArray files() As String) As String
        If files.Length > 0 Then
            If files.Length = 1 Then
                Return files(0)
            Else
                Dim startIndex As Integer = 0
                Dim maxIndex = files.Min(Function(s) s.Length)
                Dim coll As New Concurrent.ConcurrentBag(Of Integer)
                Dim result = Parallel.For(startIndex, maxIndex + 1,
                    Sub(i As Integer, state As ParallelLoopState)
                        Dim cmp = files(0).Substring(0, i)
                        With files.GetEnumerator()
                            While .MoveNext()
                                If .Current.ToString().Contains(cmp) = False Then
                                    Exit Sub
                                End If
                            End While
                        End With
                        coll.Add(i)
                    End Sub)
                While True
                    If result.IsCompleted Then
                        Return files(0).Substring(0, coll.Max())
                    End If
                End While
                Throw New Exception("Unexpected exception in function GetCommonParentDir().")
                'Dim lst As New List(Of Integer)
                'For i As Integer = startIndex To maxIndex
                '    Dim cmp = files(0).Substring(0, i)
                '    With files.GetEnumerator()
                '        While .MoveNext()
                '            If .Current.Contains(cmp) = False Then
                '                Exit For
                '            End If
                '        End While
                '    End With
                '    lst.Add(i)
                'Next
                'Return files(0).Substring(0, lst.Max())
            End If
        Else
            Return ""
        End If
    End Function

    Public Function ExtractFromStream(ByVal packageStream As Stream, ByVal lpszPath As String) As Boolean
        Try
            Dim di As New DirectoryInfo(lpszPath)
            If di.Exists Then
                di.Delete(True)
            End If
            di.Create()

#If WINDOWS_PHONE Then
            ZipConstants.DefaultCodePage = DBCSEncoding.GetDBCSEncoding("gb2312")
#ElseIf Windows And (Not NETFX_CORE) Then
            ZipConstants.DefaultCodePage = 936 ' Encoding.Default.CodePage
#End If

            ' ToDo: Add password support.
#If NETFX_CORE Then
                Using zipArch As New ZipArchive(packageStream, ZipArchiveMode.Read)
                    Dim currentPath = Me.GameContentRoot
                    For Each entry In zipArch.Entries
                        Using Input = entry.Open()
                            If entry.Name = "" And entry.FullName.EndsWith("/") Then
                                Continue For
                            Else
                                Dim fp = GetNomalizedPath(Path.Combine(Me.GameContentRoot, entry.FullName)).Replace("/", "\")
                                Using isolatedFileStream = Me.PhysicalStorage.OpenFile(fp, Runa.Foundation.IO.FileMode.OpenOrCreate)
                                    Input.CopyTo(isolatedFileStream)
                                    isolatedFileStream.Flush()
                                    isolatedFileStream.Dispose()
                                End Using
                            End If
                        End Using
                    Next
                End Using
                Return True
#Else
            Using zipStream As New ZipInputStream(packageStream)
                Using reader = New BinaryReader(zipStream)
                    Dim theEntry As ZipEntry = Nothing
                    While (Function()
                               theEntry = zipStream.GetNextEntry()
                               Return theEntry IsNot Nothing
                           End Function)()
                        Dim fileName = theEntry.Name
                        If fileName <> String.Empty Then
                            Dim path = GetNomalizedPath(System.IO.Path.Combine(lpszPath, fileName)).Replace("/", "\")
                            If (Not theEntry.IsFile) Or _
                                (Not theEntry.IsCompressionMethodSupported()) Or _
                                (Not theEntry.CanDecompress) Then
                                'Me.PhysicalStorage.CreateDirectory(path)
                                IO.Directory.CreateDirectory(path)
                            Else
                                Using isolatedFileStream = IO.File.OpenWrite(path)
                                    Using writer = New BinaryWriter(isolatedFileStream)
                                        Dim size = 2048
                                        Dim data(2048) As Byte
                                        While True
                                            size = reader.Read(data, 0, data.Length)
                                            If size > 0 Then
                                                writer.Write(data, 0, size)
                                            Else
                                                Exit While
                                            End If
                                        End While
                                    End Using
                                End Using
                            End If
                        End If
                    End While
                End Using
            End Using
            Return True
#End If
        Catch ex As Exception
            Throw
            Return False
        End Try
    End Function

    Public Sub MakeZipFile(ByVal outFilePath As String, ByVal level As Integer, pwd As String,
                           ByVal files() As String, Optional vm As CompressionPrarameterViewModel = Nothing)
        If files.Length > 0 Then
            Dim relativePath = GetCommonParentDir(files)
            If vm IsNot Nothing Then
                vm.CurrCount = files.Length
                vm.CurrIndex = 0
            End If
            If relativePath <> "" Then
                Using fs As New FileStream(outFilePath, FileMode.Create)
                    Using zs As New ZipOutputStream(fs)
                        zs.SetLevel(level)
                        zs.Password = pwd
                        For Each item In files
                            If vm IsNot Nothing Then
                                vm.CurrFile = item
                            End If
                            Call AddZipEntry(item, zs, relativePath)
                            If vm IsNot Nothing Then
                                vm.CurrIndex += 1
                            End If
                        Next
                        vm.CurrIndex = files.Length
                    End Using
                End Using
            Else
                Throw New Exception("Failed to execute function GetCommonParentDir.")
            End If
        Else
            Throw New ArgumentException("Empty file list was given to function MakeZipFile.")
        End If
    End Sub

    Public Sub AddZipEntry(ByVal item As String, ByVal zs As ZipOutputStream, Optional ByVal relativePath As String = "")
        If File.Exists(item) Then
            Dim z = New ZipEntry(GetRelativePath(relativePath, item).Substring(2))
            zs.PutNextEntry(z)
            Dim b = My.Computer.FileSystem.ReadAllBytes(item)
            zs.Write(b, 0, b.Length)
        ElseIf Directory.Exists(item) Then
            Dim di As New DirectoryInfo(item)
            Dim subDirs = di.GetDirectories()
            If subDirs.Length = 0 Then
                Dim z = New ZipEntry(GetRelativePath(relativePath, item).Substring(2) & "\")
                zs.PutNextEntry(z)
            Else
                For Each subDir In di.GetDirectories()
                    Dim z = New ZipEntry(GetRelativePath(relativePath, subDir.FullName).Substring(2) & "\")
                    zs.PutNextEntry(z)
                    Call AddZipEntry(subDir.FullName, zs, relativePath)
                Next
            End If
            For Each subFile In di.GetFiles()
                Call AddZipEntry(subFile.FullName, zs, relativePath)
            Next
        End If
    End Sub

End Module