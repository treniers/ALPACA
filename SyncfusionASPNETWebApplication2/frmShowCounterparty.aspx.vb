Imports System.Data.SqlClient



Public Class WebForm1
    Inherits System.Web.UI.Page
    Private qsCtpy As String
    Public Shared lcounterparties As IEnumerable(Of Counterparty)
    Public Shared ltest As IEnumerable(Of Counterparty)
    '    Public Shared lcounterpartyrole As IEnumerable(Of Counterparties_Role)
    'Public Shared lroleTypes As IEnumerable(Of Counterparties_RoleType)
    Public Shared objRole As Counterparties_Role

    Private Sub WebForm1_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        qsCtpy = Request.QueryString("Ctpy")
        If qsCtpy = Nothing Then qsCtpy = 2
        Dim lcounterpartyrole = From c In db.Counterparties_Roles
                                Join d In db.Counterparties_RoleTypes On c.RoleID Equals d.Id
                                Where c.CounterpartyID = qsCtpy
                                Select d.AccountType, c.Id

        '--        Select c.Id, c.CounterpartyID, c.RoleID, d.AccountType

        If lcounterpartyrole.Count > 0 Then
            With GrdRoles
                .DataSource = lcounterpartyrole
                .DataBind()
            End With

        End If
        '-- subquery for 'not in' clause in lroleTypes2
        Dim lcounterpartyroleSub = From c In db.Counterparties_Roles
                                   Where c.CounterpartyID = qsCtpy
                                   Select c.RoleID

        '            Dim lroleTypes2 As IEnumerable = From c In db.Counterparties_RoleTypes
        '            Select Case New With {Key .Id = c.Id, Key .AccountType = c.AccountType}
        Dim lroleTypes2 As IEnumerable = From c In db.Counterparties_RoleTypes
                                         Where Not lcounterpartyroleSub.Contains(c.Id)
                                         Select New With {Key .Id = c.Id, Key .AccountType = c.AccountType}

        With DropDownListAddRole
            .DataSource = lroleTypes2
            .DataValueField = "Id"
            .DataTextField = "AccountType"
            .DataBind()
        End With

    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        qsCtpy = Request.QueryString("Ctpy")
        If qsCtpy = Nothing Then qsCtpy = 2
        If Not IsPostBack() Then
            Dim lcounterparties = From c In db.Counterparties Where c.Id = qsCtpy
            Dim lroleTypes = From c In db.Counterparties_RoleTypes
            '-- populate main fields
            txtProjectNumber.Text = lcounterparties.First.ProjectNumber
            txtShortname.Text = lcounterparties.First.Shortname
            txtLongName.Text = lcounterparties.First.Longname
            Me.ButtonDeleteRole.Visible = False
            '-- metadata gridview
            Dim lmetad = From c In db.MetaDataValues
                         Join d In db.MetaDataCategories
                         On c.CategoryID Equals d.ID Where c.CounterpartyId = qsCtpy
                         Select d.MetaDCategory, c.MetaDValue, c.Period, d.ID
            If lmetad.Count > 0 Then
                With GrdMetaData
                    .AutoGenerateColumns = True
                    .DataSource = lmetad
                    .DataBind()

                End With
                With Me.qrepeater
                    .DataSource = lmetad
                    .DataBind()
                    '    FindControlRecursive(Repeater1, "ComboBox1")
                End With

            End If
        End If

        PanelAddRole.Visible = False

    End Sub

    Protected Sub SubmitButton_Click(sender As Object, e As EventArgs) Handles SubmitButton.Click

        db.SubmitChanges()
        Response.Redirect("Default.aspx")

    End Sub


    Protected Sub dlRoleTypes_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim dl As DropDownList = sender
        Dim grdV As GridViewRow = dl.Parent.Parent
        Dim selectedVal As String = dl.SelectedValue.ToString
        qsCtpy = Request.QueryString("Ctpy")
        'If Not IsPostBack() Then
        lcounterparties = From c In db.Counterparties Where c.Id = qsCtpy
        Dim lcounterpartyrole = From c In db.Counterparties_Roles Where c.Id = grdV.Cells(0).Text
        If chk_NoRoleDuplicates(selectedVal) Then

            db.Counterparties_Roles.DeleteOnSubmit(objRole)
            dl.SelectedIndex = 0

        Else
            lcounterpartyrole.First.RoleID = selectedVal

        End If

    End Sub

    Protected Sub CancelButton_Click(sender As Object, e As EventArgs)
        'db.Refresh(RefreshMode.OverwriteCurrentValues)
        Response.Redirect("Default.aspx")

    End Sub

    Function chk_NoRoleDuplicates(RoleToCheck As Integer) As Boolean
        Dim lCheck = From c In db.Counterparties_Roles
                     Where c.CounterpartyID = qsCtpy And c.RoleID = RoleToCheck
                     Group By c.RoleID Into Group
                     Where Group.Count() > 1
                     Select numberscount = Group.Count()
        If lCheck.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Function checkconnection(dx As Linq.DataContext) As Boolean
        Try
            dx.Connection.Open()
            dx.Connection.Close()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function



    Protected Sub GrdRoles_SelectedIndexChanged(sender As Object, e As EventArgs)
        If GrdRoles.SelectedIndex < 0 Then
            Me.ButtonDeleteRole.Visible = False
        Else
            Me.ButtonDeleteRole.Visible = True
        End If

    End Sub

    Protected Sub GrdRoles_RowDataBound(sender As Object, e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GrdRoles, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
            e.Row.Cells(1).Visible = False
        End If

    End Sub


    Protected Sub ButtonAddRole_Click(sender As Object, e As EventArgs) Handles ButtonAddRole.Click
        ' Dim lRolesAdd = From c In db.Counterparties_Roles
        ' Where c.CounterpartyID = qsCtpy And c.RoleID = RoleToCheck
        If Not DropDownListAddRole.SelectedIndex = -1 Then
            Dim newRole As New Counterparties_Role With {
         .CounterpartyID = qsCtpy,
         .RoleID = DropDownListAddRole.SelectedItem.Value}
            Try
                db.Counterparties_Roles.InsertOnSubmit(newRole)
                db.SubmitChanges()
            Catch
                'exception
            End Try
        End If
        addSystemMetadata(qsCtpy)

        GrdRoles.EditIndex = -1
        GrdRoles.DataBind()
        DropDownListAddRole.DataBind()

    End Sub

    Protected Sub BtnExpandNewRole_Click(sender As Object, e As EventArgs)
        PanelAddRole.Visible = True
    End Sub

    Protected Sub ButtonDeleteRole_Click(sender As Object, e As EventArgs) Handles ButtonDeleteRole.Click
        Try
            Dim ldelete = (From d In db.Counterparties_Roles Where ID = GrdRoles.SelectedRow.Cells(1).Text).FirstOrDefault
            'db.Counterparties_Roles.DeleteOnSubmit(ldelete)
            db.ExecuteCommand("delete from Counterparties_Roles where id = {0}", GrdRoles.SelectedRow.Cells(1).Text)

            'db.SubmitChanges()
        Catch
            'exception
        End Try

        GrdRoles.EditIndex = -1
        GrdRoles.DataBind()
        DropDownListAddRole.DataBind()
    End Sub

    Protected Sub txtShortname_TextChanged(sender As Object, e As EventArgs) Handles txtShortname.TextChanged
        Dim lcounterparties_edit = From c In db.Counterparties Where c.Id = qsCtpy

        With lcounterparties_edit.First
        End With
        db.SubmitChanges()
    End Sub
    Protected Sub txtProjectNumber_TextChanged(sender As Object, e As EventArgs) Handles txtProjectNumber.TextChanged
        Dim lcounterparties_edit = From c In db.Counterparties Where c.Id = qsCtpy

        With lcounterparties_edit.First

            .ProjectNumber = Me.txtProjectNumber.Text
        End With
        db.SubmitChanges()
    End Sub
    Protected Sub txtLongName_TextChanged(sender As Object, e As EventArgs) Handles txtLongName.TextChanged
        Dim lcounterparties_edit = From c In db.Counterparties Where c.Id = qsCtpy

        With lcounterparties_edit.First

            .Longname = Me.txtLongName.Text

        End With
        db.SubmitChanges()

    End Sub

    Protected Sub addSystemMetadata(sctpy As String)
        '-- subquery for 'not in' clause
        Dim lNotIn = From c In db.MetaDataValues
                     Where c.CounterpartyId = qsCtpy
                     Select c.CategoryID

        Dim lmetadata = From m In db.MetaDataCategories
                        Join d In db.MetaDataConfigPerRoles
                            On m.ID Equals d.MetaDCategoryID
                        Where Not lNotIn.Contains(m.ID)
                        Select d.MetaDCategoryID
        For Each z In lmetadata
            Dim newCat As New MetaDataValue With {
         .CounterpartyId = sctpy,
         .CategoryID = z}
            Try
                db.MetaDataValues.InsertOnSubmit(newCat)
            Catch
                'exception
            End Try
        Next
        db.SubmitChanges()
    End Sub

    Protected Sub GrdMetaData_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub GrdMetaData_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Dim lcfdsq = From c In db.MetaDataCategoriesListValues

    End Sub

    Protected Sub SqlDataSource1_DataBinding(sender As Object, e As EventArgs)

    End Sub

    Protected Sub qrepeater_DataBinding(sender As Object, e As EventArgs)

    End Sub

    Protected Sub qrepeater_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim cboValueList As Syncfusion.JavaScript.Web.ComboBox = TryCast(e.Item.FindControl("cboFillIn"), Syncfusion.JavaScript.Web.ComboBox)
            With cboValueList
                .DataSource = Me.GetData("select * from MetaDataCategoriesListValues where [UsedForCatID] =" & e.Item.DataItem.ID & "")
                .DataTextField = "ListValue"
                .DataValueField = "ID"
                .DataBind()
            End With
        End If
    End Sub


    Private Function GetData(ByVal query As String) As DataTable
        Dim constr As String = ConfigurationManager.ConnectionStrings("ALPACADBConnectionString").ConnectionString
        Using con As SqlConnection = New SqlConnection(constr)
            Using cmd As SqlCommand = New SqlCommand(query, con)
                Using sda As SqlDataAdapter = New SqlDataAdapter(cmd)
                    Dim dt As DataTable = New DataTable()
                    sda.Fill(dt)
                    Return dt
                End Using
            End Using
        End Using
    End Function
End Class