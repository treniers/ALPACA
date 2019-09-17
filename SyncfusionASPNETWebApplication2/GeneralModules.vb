Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Data.Linq
Imports System.Text

Public Module GeneralModules
    Public db As New AlpacaDBDataContext
    Public dbW As New AlpacaDBDataContext


    '    Public lcounterparties As Linq.Enumerable
    Public gSelectedCounterparty As String
    Public Function FindControlRecursive(
ByVal rootControl As Control, ByVal controlID As String) As Control

        If rootControl.ID = controlID Then
            Return rootControl
        End If

        For Each controlToSearch As Control In rootControl.Controls
            Dim controlToReturn As Control =
                FindControlRecursive(controlToSearch, controlID)
            If controlToReturn IsNot Nothing Then
                Return controlToReturn
            End If
        Next
        Return Nothing
    End Function
End Module
