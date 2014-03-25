Imports System
Imports System.IO
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Navigation

Partial Public Class TipPage
	Public Sub New()
		MyBase.New()

		Me.InitializeComponent()

		' 在此点之下插入创建对象所需的代码。
	End Sub

    Private Sub LayoutRoot_MouseEnter(sender As Object, e As MouseEventArgs) Handles LayoutRoot.MouseEnter
        Me.NavigationService.Navigate(Nothing)
    End Sub
End Class
