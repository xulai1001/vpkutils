Imports System
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class Bindable
    Implements INotifyPropertyChanged, INotifyPropertyChanging

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
    Public Event PropertyChanging(sender As Object, e As PropertyChangingEventArgs) Implements INotifyPropertyChanging.PropertyChanging

    Public Sub SetProperty(Of T)(ByRef member As T, value As T, <CallerMemberName> Optional prop_name As String = Nothing)
        RaiseEvent PropertyChanging(Me, New PropertyChangingEventArgs(prop_name))
        member = value
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(prop_name))
    End Sub
End Class
