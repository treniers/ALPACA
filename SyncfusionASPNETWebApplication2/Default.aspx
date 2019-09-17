<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="ALPACA._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <ej:ComboBox ID="ComboBox1" runat="server" Placeholder="Please select" ActionFailureTemplate="The Request Failed" CssClass="" DataSourceCachingMode="None" DataSourceID="SqlDataSource1"
                Locale="en-BE" NoRecordsTemplate="No Records Found" SortOrder="None"
                DataTextField ="Longname" DataValueField="ID">
            
        </ej:ComboBox>
        </div>
    <div>
        <ej:Kanban ID="Kanban1" runat='server' AllowScrolling="True" CssClass="" DataSourceCachingMode="None" DataSourceID="SqlDataSource1" KeyField="Status" Locale="en-BE" MinWidth="0">
            <Columns>
                <ej:KanbanColumn HeaderText="Id" Key="Id">
                </ej:KanbanColumn>
                <ej:KanbanColumn HeaderText="ProjectNumber" Key="ProjectNumber">
                </ej:KanbanColumn>
                <ej:KanbanColumn HeaderText="Shortname" Key="Shortname">
                </ej:KanbanColumn>
                <ej:KanbanColumn HeaderText="Longname" Key="Longname">
                </ej:KanbanColumn>
            </Columns>
            <Fields Content="Summary" PrimaryKey="Id">
            </Fields>
        </ej:Kanban>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ALPACADBConnectionString %>" SelectCommand="SELECT * FROM [Counterparties]"></asp:SqlDataSource>
</div>
</asp:Content>
