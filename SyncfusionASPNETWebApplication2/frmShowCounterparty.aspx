<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmShowCounterparty.aspx.vb" Inherits="ALPACA.WebForm1" %>

<%@ Register Assembly="Syncfusion.EJ.Web, Version=17.2460.0.34, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.JavaScript.Web" TagPrefix="ej" %>

<%@ Register Assembly="Syncfusion.EJ, Version=17.2460.0.34, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.JavaScript.Models" TagPrefix="ej" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="width: 100%">

        <table id="tblLayoutTopButtons" style="width: 100%">
            <tr style="width: 100%">
                <td style="width: 75%"></td>
                <td style="width: 10%">
                    <asp:Button ID="SubmitButton" Text="Save changes" runat="server" OnClick="SubmitButton_Click" CssClass="btn-dark" />
                </td>
                <td style="width: 10%">
                    <asp:Button ID="CancelButton" Text="Cancel & exit" runat="server" OnClick="CancelButton_Click" CssClass="btn-dark" />
                </td>
                <td style="width: 5%"></td>
            </tr>
        </table>
    </div>
    <div>
        <table id="tblLayout">
            <tr>
                <td style="width: 100px"></td>
                <td></td>

                <td></td>
                <td style="width: 300px"></td>
            </tr>

            <tr>
                <td style="width: 100px"></td>

                <td></td>
                <td></td>

            </tr>
            <tr>
                <td style="width: 100px"></td>

                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>

        </table>
    </div>
    <div>
        <table id="layoutsub" style="width: 100%">
            <tr>
                <%--                <td style="width: 20%"></td>
                <td>&nbsp;</td>--%>

                <td>
                    <asp:Panel ID="PanelLeft" runat="server" Style="float: left; width: 100px">
                        <br />
                        <br />
                    </asp:Panel>
                    <asp:Panel ID="PanelMiddle" runat="server" Style="float: left; width: 40%">

                        <asp:Label runat="server" for="txtProjectNumber" CssClass="badge badge-pill badge-info">Project Number</asp:Label>

                        <br />

                        <asp:TextBox ID="txtProjectNumber" runat="server" CssClass="input-group-text"></asp:TextBox>
                        <asp:Label runat="server" for="txtShortname" CssClass="badge badge-pill badge-info">Project Short Name</asp:Label>

                        <br />

                        <asp:TextBox ID="txtShortname" runat="server" CssClass="input-group-text"></asp:TextBox>
                        <asp:Label runat="server" for="txtLongName" CssClass="badge badge-pill badge-info">Project Long Name</asp:Label>

                        <br />

                        <asp:TextBox ID="txtLongName" runat="server" CssClass="input-group-text"></asp:TextBox>

                    </asp:Panel>
                    <asp:Panel ID="Panel1" runat="server" Style="float: right; width: 50%">
                        <asp:Label ID="Label1" runat="server" Text="Counterparty Roles" CssClass="badge badge-pill badge-info"></asp:Label>
                        <asp:GridView ID="GrdRoles" runat="server" CssClass="table-striped table-hover" OnSelectedIndexChanged="GrdRoles_SelectedIndexChanged" OnRowDataBound="GrdRoles_RowDataBound" ShowHeader="false">
                            <SelectedRowStyle BackColor="#FFCC00" />
                        </asp:GridView>
                        <asp:Button ID="BtnExpandNewRole" runat="server" OnClick="BtnExpandNewRole_Click" Text="Add Role" Width="65px" />
                        <asp:Button ID="ButtonDeleteRole" runat="server" Text="Delete Role" Width="73px" />
                        <asp:Panel ID="PanelAddRole" runat="server" Height="133px" Width="149px">
                            <asp:DropDownList ID="DropDownListAddRole" runat="server">
                            </asp:DropDownList>
                            <asp:Button ID="ButtonAddRole" runat="server" Height="29px" Text="Add" Width="60px" />
                        </asp:Panel>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server" Style="float: left; width: 100px">
                        <br />
                        <br />
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" Style="float: left; width: 40%">
                        <asp:Label ID="Label2" runat="server" Text="MetaData" CssClass="badge badge-pill badge-info"></asp:Label>
                        <asp:GridView ID="GrdMetaData" runat="server" CssClass="table-striped table-hover">
                            <SelectedRowStyle BackColor="#FFCC00" />
                        </asp:GridView>

                        <%--                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ALPACADBConnectionString %>" SelectCommand="SELECT * FROM [MetaDataCategoriesListValues] WHERE ([UsedForCatID] = @UsedForCatID)"  >
                            <SelectParameters>
                                <asp:Parameter DefaultValue='1234' Name="UsedForCatID" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        --%>
                        <asp:Repeater ID="qrepeater" runat="server" OnItemDataBound="qrepeater_ItemDataBound">
                            <ItemTemplate>
                                <%--                        <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="ALPACA.AlpacaDBDataContext" EntityTypeName="" TableName="MetaDataCategoriesListValues" Where="UsedForCatID ==" <%# Eval(Container.DataItem, "ID") %>'>--%>

                                <ej:ComboBox ID="cboFillIn" runat="server" Placeholder="Select please" AllowCustom="false">
                                </ej:ComboBox>
                            </ItemTemplate>

                        </asp:Repeater>


                        <ej:ComboBox ID="ComboBox1" runat="server" DataSourceID="SqlDataSource1" DataValueField="ID"></ej:ComboBox>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ALPACADBConnectionString %>" SelectCommand="SELECT * FROM [MetaDataValues]"></asp:SqlDataSource>
                    </asp:Panel>
                </td>

            </tr>
        </table>
    </div>
    <div>
        <table style="width: 100%">
            <tr>
                <td style="width: 20%"></td>
                <td></td>

                <td>&nbsp;</td>
            </tr>
        </table>
    </div>



</asp:Content>
