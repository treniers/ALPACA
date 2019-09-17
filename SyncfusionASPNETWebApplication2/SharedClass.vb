Imports System.Reflection

Public Class SharedClass
    Public Shared Sub ToHtmlTable(Of T)(ByVal list As IEnumerable(Of T), ByVal tbl As HtmlTable, Optional AddDeleteBtn As Boolean = False, Optional AddEditButton As Boolean = False)
        Dim type = GetType(T)
        Dim props = type.GetProperties()
        Dim rowcnt As Integer = 0
        'Dim i As IEnumerator
        'Dim z As IEnumerator
        'Debug.Print(p.Name)
        For Each i In list
            Dim srow As New HtmlTableRow()
            'For Each p In props
            'Debug.Print(p.Name)
            rowcnt = rowcnt + 1
            For Each c In props.Select(Function(s) s.GetValue(i)).ToList()

                Dim scell As New HtmlTableCell()
                scell.InnerHtml = c.ToString
                ' scell.Controls.Add(New LiteralControl(scell.InnerHtml))
                With srow
                    .Cells.Add(scell)

                End With
            Next

            If AddDeleteBtn = True Then
                Dim scell As New HtmlTableCell()
                Dim btn As New Button
                btn.ID = "btnDelete_" & tbl.ID.ToString & "_" & rowcnt.ToString
                btn.Text = "Delete me"
                btn.OnClientClick = "BtnDelete_" & tbl.ID.ToString & "_Click"
                scell.Controls.Add(btn)
                srow.Cells.Add(scell)
            End If

            If AddEditButton = True Then
                Dim scell As New HtmlTableCell()
                Dim btn As New Button
                btn.ID = "btnEdit_" & tbl.ID.ToString & "_" & rowcnt.ToString
                btn.Text = "Edit me"
                btn.OnClientClick = "BtnEdit_" & tbl.ID.ToString & "_Click"
                '                AddHandler btn.Click, AddressOf WebForm1.BtnEdit_tblRoles_Click
                '                AddHandler btn.Click, AddressOf "WebForm1.BtnEdit_" & tbl.ID.ToString & "_Click"
                Dim dtype As Type = GetType(WebForm1)
                Dim methodi As MethodInfo = dtype.GetMethod("BtnEdit_tblRoles_Click")
                'AddHandler btn.Click,
                scell.Controls.Add(btn)
                srow.Cells.Add(scell)
            End If

            tbl.Rows.Add(srow)

        Next

    End Sub
End Class
